using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lo_novo
{
    public enum EatsNoun
    {
        Zero,
        One,
        Two
    }

    public class RoomParser
    {
        protected Room room;

        // yuck
        string debug = "";

        public RoomParser(Room r) { room = r; }

        public void Parse(string input, string defaultResponse = "I don't know how to do that.")
        {
            if (!TryParse(input))
                State.Player.IRC.Send("I don't know how to do that. (Expected form is 'verb (noun) ((with|at|to|...) noun).) Try typing 'help' for ideas." + ((debug != "") ? "\n{{" + debug + "}}" : ""));
        }

        public bool TryParse(string input)
        {
            // 0. cull useless words for default parsing
            var s = input.ToLower().Trim().Replace("?", "").Replace("!", "").Replace(",", "").Replace(";", "");
            var sbits = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            debug = "";

            if (s.Length == 0)
                return true; // do nothing

            while (sbits.Count > 0 && new string[] { "go", "do", "then" }.Contains(sbits[0]))
                sbits.RemoveAt(0);

            if (sbits.Count == 0)
                return false;

            // 1. feed into the default verb parser, to try and get hints about intention
            // longest possible first!
            Intention intent = null;
            string eaten = null;
            bool gotVerb = false;

            foreach (var eats in sbits.JoinedAndSplit())
            {
                if (gotVerb)
                    break;

                intent = VerbClassifier.Verbify(eats);
                if (intent.DefaultVerb != DefaultVerb.DontKnow)
                {
                    eaten = eats;
                    gotVerb = true;
                }
            }

            Func<Intention, bool> customRule = null;
            var alreadyEatenActiveNoun = false;
            var alreadyEatenBothNouns = false;
            bool gotVerb2 = false;

            // 2. Parse against any custom actions in room script, longest first
            foreach (var eatstring in sbits.JoinedAndSplit())
            {
                if (gotVerb2)
                    break;

                foreach (var rule in room.CustomRegex)
                {
                    if (rule.Item1 != rule.Item1.ToLowerInvariant())
                        State.SystemMessage("warning - custom rule " + rule.Item1 + " isn't equal to (rule).ToLowerInvariant(). user input is always matched in lowercase");

                    if (Regex.IsMatch(eatstring, rule.Item1))
                    {
                        var matches = Regex.Matches(eatstring, rule.Item1);

                        gotVerb = true;
                        gotVerb2 = true;

                        switch (rule.Item2)
                        {
                            case EatsNoun.Zero:
                                break;

                            case EatsNoun.One:
                                alreadyEatenActiveNoun = true;
                                break;

                            case EatsNoun.Two:
                                alreadyEatenActiveNoun = true;
                                alreadyEatenBothNouns = true;
                                break;
                        }

                        intent.RegexMatches = matches;
                        intent.DefaultVerb = DefaultVerb.DontKnow;
                        intent.VerbString = rule.Item1;

                        customRule = rule.Item3;
                        break;
                    }
                }
            }


            // 3. If no verb yet, try and choose one from system keywords
            if (!gotVerb)
            {
                // try 2 words so e.g. 'x bag' or 'x inventory' works.
                for (int i = 0; i < (2 <= sbits.Count ? 2 : sbits.Count); i++)
                {
                    switch (sbits[i])
                    {
                        case "save":
                        case "load":
                            State.SystemMessage("Can't save/load yet.");
                            return true;

                        case "help":
                            State.SystemMessage("IN-GAME HELP:");
                            State.SystemMessage("Expected commands should be in form 'verb (noun) (with|to|at|... noun)'.");
                            State.SystemMessage("Nouns can be something in the room, a player, yourself, or something in your inventory. Occasionally abstract concepts.");
                            State.SystemMessage("Verbs are like: north, forward, activate, attack, push, talk, take, throw, open, close, stop, climb, climb down, repair, look at, examine.");
                            State.SystemMessage("Sometimes other things are possible. Report things that you think should work but don't to the creator of the game.");
                            State.SystemMessage("Check !help for system-level help. (Leaving the party, etc.)");
                            return true;

                        case "quit":
                        case "exit":
                        case "leave":
                            State.SystemMessage("Player management is out-of-band. Use !help to see relevant commands.");
                            return true;

                        case "xtravel":
                        case "xteleport":
                            State.SystemMessage("Teleporting...");

                            Type roomType = null;

                            for (int k = i + 1; k < sbits.Count; k++)
                            {
                                foreach (var pre in new string[] { "lo_novo.", "lo_novo.Game.", "" })
                                {
                                    roomType = Type.GetType(pre + sbits[k], false, true);
                                    if (roomType != null)
                                        break;
                                }
                                if (roomType != null)
                                    break;
                            }

                            if (roomType != null)
                            {
                                try
                                {
                                    State.Travel(roomType);
                                }
                                catch (Exception e)
                                {
                                    State.SystemMessage("Teleport failed: " + e.Message + 
                                        (e.InnerException != null ? "\nInnerException: " + e.InnerException : ""));
                                }
                            }
                            else
                                State.SystemMessage("Failed to teleport " + State.Player.Name + ": couldn't find Type.");

                            return true;

                        default:
                            // maybe it's inventory management?
                            if (InventoryParser.Parse(sbits[i], sbits))
                                return true;
                            break;
                    }
                }
            }

            // 4. Still no verb? Give up.
            if (!gotVerb)
            {
                debug = "no verb";
                return false;
            }

            // 5. We should have at least a verb now. Discard verb from search *STRING*.
            // TODO: err this replaces all instances
            s = string.Join(" ", sbits).Replace(eaten, "").Trim();

            // 6. are we searching for the active or the passive noun?
            string[] activeNounBits = null;
            string[] passiveNounBits = null;

            // if we only find a passive noun, should that actually be an active one?
            var dubiousRelationship = false;

            // !!! let us break out later if we see a noun relationship but not a noun
            var expectNoun = false;

            if (!alreadyEatenBothNouns)
            {
                // active vs. passive relationship is dodgy...
                // do we need to change this to be verb-aware?

                var found = false;

                foreach (var activePassiveRelation in new string[] { "with", "towards?",
                    "from", "away", "away from" })
                {
                    if (Regex.IsMatch(s, " " + activePassiveRelation + " "))
                    {
                        var m = Regex.Match(s, " " + activePassiveRelation + " ");
                        if (!alreadyEatenActiveNoun)
                            activeNounBits = s.Substring(0, s.IndexOf(m.Value)).Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        passiveNounBits = s.Substring(s.IndexOf(m.Value) + m.Value.Length).Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        s = s.Replace(m.Value, "");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    // active-passive relationships that are a bit dubious
                    foreach (var activePassiveRelation in new string[] { "at", "away to", "down on",
                        "somewhere (else )?like", "to"})
                    {
                        if (Regex.IsMatch(s, " " + activePassiveRelation + " "))
                        {
                            var m = Regex.Match(s, " " + activePassiveRelation + " ");
                            if (!alreadyEatenActiveNoun)
                                activeNounBits = s.Substring(0, s.IndexOf(m.Value)).Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            passiveNounBits = s.Substring(s.IndexOf(m.Value) + m.Value.Length).Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            s = s.Replace(m.Value, "");
                            dubiousRelationship = true;
                            found = true;
                            break;
                        }
                    }
                }

                // TODO: ... passiveActiveRelation ...

                // just an active verb, then?
                if (!found)
                    activeNounBits = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (found)
                    expectNoun = true;

                // XXX: we should err on the side of expecting a noun, maybe?
                // expectNoun is weird anyway. If there are no words, we try to dispatch regardless.
                // If there are words we can't parse as nouns though, we'll complain.
                if (customRule == null)
                    expectNoun = true;

            }

            // 6.5. escape hatch for dictionary
            if (intent.DefaultVerb == DefaultVerb.SystemDictionary)
            {
                if (activeNounBits == null || activeNounBits.Length < 1)
                {
                    State.o("What term would you like me to define?");
                    return true;
                }
                else
                {
                    var done = false;

                    foreach (var word in activeNounBits.JoinedAndSplit())
                    {
                        if (done)
                            break;

                        var ws = new List<string>();
                        ws.Add(word);

                        if (word.EndsWith("s"))
                            ws.Add(word.Substring(0, word.Length - 1));

                        foreach (var w in ws)
                            if (WordDictionary.Dict.ContainsKey(w))
                            {
                                State.o(WordDictionary.Dict[w]);
                                done = true;
                            }
                    }

                    /*if (!done)
                        State.o("It's complicated. But you'll figure it out.");*/

                    if (done)
                        return true;

                    // demote to look; maybe room has something specific for this.
                    intent.DefaultVerb = DefaultVerb.Look;
                }
            }


            // 7. search remaining words for active & passive noun
            if (!alreadyEatenBothNouns)
            {
                var nouns = NounLibrary.Build();

                if (!alreadyEatenActiveNoun && activeNounBits != null && activeNounBits.Length > 0)
                {
                    foreach (var nmaybe in activeNounBits.JoinedAndSplit())
                        foreach (var n in nouns)
                        {
                            if (intent.ActiveNoun != null)
                                break;

                            if (Regex.IsMatch(nmaybe, n.Item1))
                            {
                                intent.ActiveNoun = n.Item2;
                                intent.ActiveNounString = Regex.Match(nmaybe, n.Item1).Value;
                            }
                        }

                    if (expectNoun && intent.ActiveNoun == null)
                    {
                        State.o("I don't know which noun '" + string.Join(" ", activeNounBits) + "' means. [a]");
                        return true;
                    }
                }

                if (passiveNounBits != null && passiveNounBits.Length > 0)
                {
                    foreach (var nmaybe in passiveNounBits.JoinedAndSplit())
                        foreach (var n in nouns)
                        {
                            if (intent.PassiveNoun != null)
                                break;

                            if (Regex.IsMatch(nmaybe, n.Item1))
                            {
                                intent.PassiveNoun = n.Item2;
                                intent.PassiveNounString = Regex.Match(nmaybe, n.Item1).Value;
                            }
                        }

                    if (expectNoun && intent.PassiveNoun == null)
                    {
                        State.o("I don't know which noun '" + string.Join(" ", passiveNounBits) + "' means. [p]");
                        return true;
                    }
                }

                // if we only found a passive noun, and the active-passive relationship found was
                //  of dubious merit to begin with, we probably actually found the active noun.
                if (dubiousRelationship && intent.ActiveNoun == null && intent.PassiveNoun != null)
                {
                    intent.ActiveNoun = intent.PassiveNoun;
                    intent.ActiveNounString = intent.PassiveNounString;
                    intent.PassiveNoun = null;
                    intent.PassiveNounString = "";
                    State.Player.IRC.Send("{{dubious relationship hack}}");
                }

            }


            // !!! TODO: ADVERBS, ADJECTIVES !!!
            // (don't care about actually mapping them to which object,
            //  just want to influence Intention stats)


            // 7.999. Let Room modify the Intention now, if it wishes to.
            intent = State.Room.ParsePostprocess(intent, input);


            // 8. Dispatch actions. Order:
            //  (a) Custom action if any
            //  (1.1854138) Room's EarlyDispatch function
            //  (b) Room script
            //  (c) Active noun script
            //  (d) Default room script if no active noun/noun is in room
            //  (e) Specific item script if active noun is in room
            //  (f) Default inventory item script if active noun in inventory
            //  (g) Default room script regardless of the nouns
            //  (h) Give up.

            if (customRule != null)
                if (customRule(intent))
                    return true;

            if (State.Room.EarlyDispatch(intent))
                return true;

            if (intent.DispatchOnIObey(State.Room))
                return true;

            if (intent.ActiveNoun != null && intent.ActiveNoun is IObey && intent.DispatchOnIObey(intent.ActiveNoun as IObey))
                return true;

            if ((intent.ActiveNoun == null || State.Room.Contents.Contains(intent.ActiveNoun as Thing))
                && intent.DispatchOnIObey(State.Room.DefaultRoomResponses))
                return true;

            if (intent.ActiveNoun != null && State.Room.Contents.Contains(intent.ActiveNoun as Thing)
                && intent.DispatchOnIObey(intent.ActiveNoun as Thing))
                return true;

            if (intent.ActiveNoun != null && State.Player.Inventory.Contains(intent.ActiveNoun as Thing)
                && intent.DispatchOnIObey(State.Player.DefaultInventoryResponses))
                return true;

            if (intent.DispatchOnIObey(State.Room.DefaultRoomResponses))
                return true;

            debug = "i understood you, but nothing accepted the dispatch";
            return false;
        }
    }
}
