using System;
using System.Collections.Generic;

namespace lo_novo
{
    public static class VerbClassifier
    {
        private static void si(Intention i, DefaultVerb pv, int air, int violent, int whimsy)
        {
            // all the stats fields basically have 4 possible values:
            // 0, 1, 5 or 10. 0 is 'not at all', 1 is 'not especially', and 10 is 'very much so'.
            i.DefaultVerb = pv;
            i.Airborne = air;
            i.Violence = violent;
            i.Whimsy = whimsy;
        }

        public static Intention Verbify(string s)
        {
            var i = new Intention();
            i.VerbString = s.ToLower().Trim();
            var v = i.VerbString.Replace('-', ' ');

            // TODO: regexp!

            switch (v)
            {
                case "add":
                    // add to inventory, or add part?
                    si(i, DefaultVerb.Take, 1, 1, 1);
                    break;

                case "arrest":
                    si(i, DefaultVerb.Stop, 0, 5, 5);
                    break;

                case "halt":
                    si(i, DefaultVerb.Stop, 0, 1, 1);
                    break;

                case "press":
                    si(i, DefaultVerb.Activate, 1, 1, 1);
                    break;

                case "activate":
                    si(i, DefaultVerb.Activate, 1, 1, 1);
                    break;

                case "flick":
                    si(i, DefaultVerb.Activate, 1, 1, 5);
                    break;

                case "flip":
                    si(i, DefaultVerb.Activate, 1, 1, 1);
                    break;

                case "turn on":
                    si(i, DefaultVerb.Activate, 1, 1, 1);
                    break;

                case "poke":
                    si(i, DefaultVerb.Push, 0, 1, 10);
                    break;

                case "prod":
                    si(i, DefaultVerb.Attack, 1, 1, 10);
                    break;

                case "kick":
                    si(i, DefaultVerb.Attack, 5, 5, 2);
                    break;

                case "punch":
                    si(i, DefaultVerb.Attack, 1, 10, 1);
                    break;

                case "uppercut":
                case "upper cut":
                case "falconpunch":
                case "falcon punch":
                    si(i, DefaultVerb.Attack, 10, 10, 3);
                    break;

                case "murder":
                    si(i, DefaultVerb.Attack, 0, 10, 5);
                    break;

                case "destroy":
                    si(i, DefaultVerb.Attack, 1, 10, 0);
                    break;

                case "headbutt":
                    si(i, DefaultVerb.Attack, 1, 5, 5);
                    break;

                case "roundhouse":
                case "round house":
                    si(i, DefaultVerb.Attack, 1, 10, 3);
                    break;

                case "piledrive":
                case "piledriver":
                    si(i, DefaultVerb.Attack, 0, 10, 5);
                    break;

                case "wrestle":
                    si(i, DefaultVerb.Attack, 1, 5, 1);
                    break;

                case "assault":
                    si(i, DefaultVerb.Attack, 1, 10, 5);
                    break;

                case "attack":
                    si(i, DefaultVerb.Attack, 1, 10, 1);
                    break;

                case "rip":
                    si(i, DefaultVerb.Attack, 5, 10, 10);
                    break;

                case "pluck":
                    si(i, DefaultVerb.Attack, 1, 1, 10);
                    break;

                case "rend":
                    si(i, DefaultVerb.Attack, 3, 10, 5);
                    break;

                case "dislocate":
                    si(i, DefaultVerb.Attack, 1, 10, 10);
                    break;

                case "strangle":
                    si(i, DefaultVerb.Attack, 1, 10, 3);
                    break;

                case "push":
                    si(i, DefaultVerb.Push, 1, 1, 1);
                    break;

                case "move":
                    si(i, DefaultVerb.Push, 1, 1, 1);
                    break;

                case "drive":
                    si(i, DefaultVerb.Push, 1, 1, 2);
                    break;

                case "shove":
                    si(i, DefaultVerb.Push, 1, 5, 1);
                    break;

                case "nudge":
                    si(i, DefaultVerb.Push, 1, 1, 5);
                    break;

                case "exert force":
                    si(i, DefaultVerb.Push, 2, 5, 2);
                    break;

                case "jolt":
                    si(i, DefaultVerb.Push, 1, 10, 2);
                    break;

                case "propel":
                    si(i, DefaultVerb.Push, 5, 1, 5);
                    break;

                case "pull":
                    si(i, DefaultVerb.Pull, 1, 1, 1);
                    break;

                case "haul":
                    si(i, DefaultVerb.Pull, 5, 3, 3);
                    break;

                case "drag":
                case "lug":
                case "heave":
                    si(i, DefaultVerb.Pull, 1, 5, 5);
                    break;

                case "yank":
                    si(i, DefaultVerb.Pull, 3, 5, 10);
                    break;

                case "tow":
                    si(i, DefaultVerb.Pull, 1, 1, 3);
                    break;

                case "chitchat":
                case "chit chat":
                case "natter":
                    si(i, DefaultVerb.Talk, 1, 1, 5);
                    break;

                case "ask":
                case "chat":
                case "discuss":
                case "talk":
                case "question":
                    si(i, DefaultVerb.Talk, 1, 1, 1);
                    break;

                case "converse":
                case "enquire":
                case "query":
                case "whisper":
                    si(i, DefaultVerb.Talk, 1, 1, 3);
                    break;
                
                case "shout":
                case "thunder":
                case "exclaim":
                case "rage":
                    si(i, DefaultVerb.Talk, 1, 5, 5);
                    break;

                case "insult":
                    si(i, DefaultVerb.Talk, 1, 10, 5);
                    break;

                case "take":
                case "pick up":
                case "pick":
                    si(i, DefaultVerb.Take, 1, 1, 1);
                    break;

                case "grab":
                case "yoink":
                case "steal":
                case "wrench":
                    si(i, DefaultVerb.Take, 1, 10, 5);
                    break;


                // OK BORED NOW
                case "throw":
                    si(i, DefaultVerb.Punt, 10, 5, 1);
                    break;

                case "punt":
                    si(i, DefaultVerb.Punt, 5, 5, 10);
                    break;

                case "stop":
                    si(i, DefaultVerb.Stop, 0, 2, 1);
                    break;

                case "open":
                    si(i, DefaultVerb.Open, 1, 1, 1);
                    break;

                case "close":
                case "shut":
                    si(i, DefaultVerb.Close, 1, 1, 1);
                    break;

                case "climb up":
                case "climb":
                case "up":
                case "go up":
                    si(i, DefaultVerb.Climb, 1, 1, 1);
                    break;

                case "clamber up":
                case "clamber":
                    si(i, DefaultVerb.Climb, 2, 1, 5);
                    break;

                case "climb down":
                case "descend":
                case "down":
                case "go down":
                    si(i, DefaultVerb.Descend, 1, 1, 1);
                    break;

                case "clamber down":
                    si(i, DefaultVerb.Descend, 2, 1, 5);
                    break;

                case "modify":
                    si(i, DefaultVerb.Modify, 1, 1, 1);
                    break;

                case "repair":
                case "fix":
                    si(i, DefaultVerb.Modify, 1, 1, 1);
                    break;

                case "look at":
                case "look":
                case "examine":
                case "peer at":
                case "take a closer look at":
                case "x":
                    si(i, DefaultVerb.Look, 1, 1, 1);
                    break;

                default:
                    i.DefaultVerb = DefaultVerb.DontKnow;
                    break;
            }

            return i;
        }
    }
}

