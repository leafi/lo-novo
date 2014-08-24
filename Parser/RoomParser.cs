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
            IEnumerable<string> eaten = null;
            bool gotVerb = false;

            for (int i = sbits.Count; i > 0; i--)
                for (int j = 0; j < sbits.Count - i + 1; j++)
                {
                    if (gotVerb)
                        break;

                    var eats = sbits.Skip(j).Take(i);
                    intent = VerbClassifier.Verbify(string.Join(" ", eats));
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
            for (int i = sbits.Count; i > 0; i--)
                for (int j = 0; j < sbits.Count - i + 1; j++)
                {
                    if (gotVerb2)
                        break;

                    var eats = sbits.Skip(j).Take(i);
                    var eatstring = string.Join(" ", eats);

                    foreach (var rule in room.CustomRegex)
                    {
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
            s = s.Replace(string.Join(" ", eaten), "").Trim();


            // 6. are we searching for the active or the passive noun?
            string[] activeNounBits = null;
            string[] passiveNounBits = null;

            // if we only find a passive noun, should that actually be an active one?
            var dubiousRelationship = false;

            if (!alreadyEatenBothNouns)
            {
                // active vs. passive relationship is dodgy...
                // do we need to change this to be verb-aware?

                var found = false;

                foreach (var activePassiveRelation in new string[] { "with", "towards?", "to",
                    "from", "away", "away from" })
                {
                    if (Regex.IsMatch(s, activePassiveRelation))
                    {
                        var m = Regex.Match(s, activePassiveRelation + " ");
                        if (!alreadyEatenActiveNoun)
                            activeNounBits = s.Substring(0, s.IndexOf(m.Value)).Trim().Split(' ');
                        passiveNounBits = s.Substring(s.IndexOf(m.Value) + m.Value.Length).Trim().Split(' ');
                        s = s.Replace(m.Value, "");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    // active-passive relationships that are a bit dubious
                    foreach (var activePassiveRelation in new string[] { "at", "away to", "down on",
                        "somewhere (else )?like"})
                    {
                        if (Regex.IsMatch(s, activePassiveRelation))
                        {
                            var m = Regex.Match(s, activePassiveRelation + " ");
                            if (!alreadyEatenActiveNoun)
                                activeNounBits = s.Substring(0, s.IndexOf(m.Value)).Trim().Split(' ');
                            passiveNounBits = s.Substring(s.IndexOf(m.Value) + m.Value.Length).Trim().Split(' ');
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
                    activeNounBits = s.Split(' ');
            }


            // 7. search remaining words for active & passive noun
            if (!alreadyEatenBothNouns)
            {
                var nouns = NounLibrary.Build();

                if (!alreadyEatenActiveNoun && activeNounBits != null)
                {
                    for (int i = activeNounBits.Length; i > 0; i--)
                        for (int j = 0; j < activeNounBits.Length - i + 1; j++)
                            foreach (var n in nouns)
                            {
                                if (intent.ActiveNoun != null)
                                    break;

                                var nmaybe = string.Join(" ", activeNounBits.Skip(j).Take(i));
                                if (Regex.IsMatch(nmaybe, n.Item1))
                                {
                                    intent.ActiveNoun = n.Item2;
                                    intent.ActiveNounString = Regex.Match(nmaybe, n.Item1).Value;
                                }
                            }
                }

                if (passiveNounBits != null)
                {
                    for (int i = passiveNounBits.Length; i > 0; i--)
                        for (int j = 0; j < passiveNounBits.Length - i + 1; j++)
                            foreach (var n in nouns)
                            {
                                if (intent.PassiveNoun != null)
                                    break;

                                var nmaybe = string.Join(" ", passiveNounBits.Skip(j).Take(i));
                                if (Regex.IsMatch(nmaybe, n.Item1))
                                {
                                    intent.PassiveNoun = n.Item2;
                                    intent.PassiveNounString = Regex.Match(nmaybe, n.Item1).Value;
                                }
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


            // 8. Dispatch actions. Order:
            //  (a) Custom action if any
            //  (b) Room script
            //  (c) Active noun script
            //  (d) Default room script if no active noun/noun is in room
            //  (e) Default inventory item script if active noun in inventory
            //  (f) Default room script regardless of the nouns
            //  (g) Give up.

            if (customRule != null)
                if (customRule(intent))
                    return true;

            if (intent.DispatchOnIObey(State.Room))
                return true;

            if (intent.ActiveNoun != null && intent.ActiveNoun is IObey && intent.DispatchOnIObey(intent.ActiveNoun as IObey))
                return true;

            if ((intent.ActiveNoun == null || State.Room.Contents.Contains(intent.ActiveNoun as Thing))
                && intent.DispatchOnIObey(State.Room.DefaultRoomResponses))
                return true;

            if (intent.ActiveNoun != null && State.Player.Inventory.Contains(intent.ActiveNoun as Thing)
                && intent.DispatchOnIObey(State.Player.DefaultInventoryResponses))
                return true;

            if (intent.DispatchOnIObey(State.Room.DefaultRoomResponses))
                return true;

            debug = "nothing handled the dispatch";
            return false;
        }
    }
}
