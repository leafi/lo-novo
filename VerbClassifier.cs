using System;
using System.Collections.Generic;

namespace lo_novo
{
    // it's ok for a thing to count as multiple thingtypes
    // even seemingly contradictory things probably aren't - a dodgy robot has both animate
    //  and inanimate qualities.
    // *** NOT IN USE - DISCARD/MOVE SOMEWHERE ELSE? ***
    [Flags]
    public enum ThingType
    {
        Animate = 1,
        Inanimate = 2,
        Clutter = 4,
        Light = 8,
        Heavy = 16,
        Modifiable = 32,
        Fixed = 64,
        CanActivate = 128,
        Openable = 256,
        Climbable = 512
    }

    public enum PrimaryVerb
    {
        Activate,
        Attack,
        Push,
        Pull,
        Talk,
        Take,
        Punt,
        Stop,
        Open,
        Close,
        Climb,
        Descend,
        Modify,
        Look,
        Go,
        DontKnow
    }

    public class Intention
    {
        // all the following stats fields basically have 4 possible values:
        // 0, 1, 5 or 10. 0 is 'not at all', 1 is 'not especially', 10 is 'very much so'
        public int Violence = 1;
        public int Airborne = 1;
        public int Considerate = 1;
        public int Whimsy = 1;

        public PrimaryVerb Verb;
        public ThingType ThingType;
        public string OriginalVerb;
    }

    public static class VerbClassifier
    {
        private static void si(Intention i, PrimaryVerb pv, int air, int considerate, int violent, int whimsy)
        {
            i.Verb = pv;
            i.Airborne = air;
            i.Considerate = considerate;
            i.Violence = violent;
            i.Whimsy = whimsy;
        }

        public static Intention Verbify(ThingType t, string s)
        {
            var v = s.ToLower().Trim().Replace('-', ' ');
            var i = new Intention();

            i.OriginalVerb = s.Trim();
            i.ThingType = t;

            switch (v)
            {
                case "add":
                    // add to inventory, or add part?
                    si(i, PrimaryVerb.Take, 1, 5, 1, 1);
                    break;

                case "arrest":
                    si(i, PrimaryVerb.Stop, 0, 5, 5, 5);
                    break;

                case "halt":
                    si(i, PrimaryVerb.Stop, 0, 5, 1, 1);
                    break;

                case "press":
                    si(i, PrimaryVerb.Activate, 1, 5, 1, 1);
                    break;

                case "poke":
                    si(i, PrimaryVerb.Push, 0, 1, 1, 10);
                    break;

                case "prod":
                    si(i, PrimaryVerb.Attack, 1, 1, 1, 10);
                    break;

                case "kick":
                    si(i, PrimaryVerb.Attack, 5, 0, 5, 2);
                    break;

                case "punch":
                    si(i, PrimaryVerb.Attack, 1, 1, 10, 1);
                    break;

                case "uppercut":
                case "upper cut":
                case "falconpunch":
                case "falcon punch":
                    si(i, PrimaryVerb.Attack, 10, 0, 10, 3);
                    break;

                case "murder":
                    si(i, PrimaryVerb.Attack, 0, 0, 10, 5);
                    break;

                case "destroy":
                    si(i, PrimaryVerb.Attack, 1, 0, 10, 0);
                    break;

                case "headbutt":
                    si(i, PrimaryVerb.Attack, 1, 0, 5, 5);
                    break;

                case "roundhouse":
                case "round house":
                    si(i, PrimaryVerb.Attack, 1, 0, 10, 3);
                    break;

                case "piledrive":
                case "piledriver":
                    si(i, PrimaryVerb.Attack, 0, 0, 10, 5);
                    break;

                case "wrestle":
                    si(i, PrimaryVerb.Attack, 1, 5, 5, 1);
                    break;

                case "assault":
                    si(i, PrimaryVerb.Attack, 1, 1, 10, 5);
                    break;

                case "attack":
                    si(i, PrimaryVerb.Attack, 1, 1, 10, 1);
                    break;

                case "rip":
                    si(i, PrimaryVerb.Attack, 5, 0, 10, 10);
                    break;

                case "pluck":
                    si(i, PrimaryVerb.Attack, 1, 1, 1, 10);
                    break;

                case "rend":
                    si(i, PrimaryVerb.Attack, 3, 0, 10, 5);
                    break;

                case "dislocate":
                    si(i, PrimaryVerb.Attack, 1, 0, 10, 10);
                    break;

                case "strangle":
                    si(i, PrimaryVerb.Attack, 1, 0, 10, 3);
                    break;

                case "push":
                    si(i, PrimaryVerb.Push, 1, 1, 1, 1);
                    break;

                case "move":
                    si(i, PrimaryVerb.Push, 1, 5, 1, 1);
                    break;

                case "drive":
                    si(i, PrimaryVerb.Push, 1, 5, 1, 2);
                    break;

                case "shove":
                    si(i, PrimaryVerb.Push, 1, 1, 5, 1);
                    break;

                case "nudge":
                    si(i, PrimaryVerb.Push, 1, 5, 1, 5);
                    break;

                case "exert force":
                    si(i, PrimaryVerb.Push, 2, 1, 5, 2);
                    break;

                case "jolt":
                    si(i, PrimaryVerb.Push, 1, 1, 10, 2);
                    break;

                case "propel":
                    si(i, PrimaryVerb.Push, 5, 5, 1, 5);
                    break;

                case "pull":
                    si(i, PrimaryVerb.Pull, 1, 1, 1, 1);
                    break;

                case "haul":
                    si(i, PrimaryVerb.Pull, 5, 1, 3, 3);
                    break;

                case "drag":
                case "lug":
                case "heave":
                    si(i, PrimaryVerb.Pull, 1, 1, 5, 5);
                    break;

                case "yank":
                    si(i, PrimaryVerb.Pull, 3, 0, 5, 10);
                    break;

                case "tow":
                    si(i, PrimaryVerb.Pull, 1, 5, 1, 3);
                    break;

                case "chitchat":
                case "chit chat":
                case "natter":
                    si(i, PrimaryVerb.Talk, 1, 1, 1, 5);
                    break;

                case "ask":
                case "chat":
                case "discuss":
                case "talk":
                case "question":
                    si(i, PrimaryVerb.Talk, 1, 1, 1, 1);
                    break;

                case "converse":
                case "enquire":
                case "query":
                case "whisper":
                    si(i, PrimaryVerb.Talk, 1, 5, 1, 3);
                    break;
                
                case "shout":
                case "thunder":
                case "exclaim":
                case "rage":
                    si(i, PrimaryVerb.Talk, 1, 1, 5, 5);
                    break;

                case "insult":
                    si(i, PrimaryVerb.Talk, 1, 0, 10, 5);
                    break;

                case "take":
                case "pick up":
                case "pick":
                    si(i, PrimaryVerb.Take, 1, 5, 1, 1);
                    break;

                case "grab":
                case "yoink":
                case "steal":
                case "wrench":
                    si(i, PrimaryVerb.Take, 1, 0, 10, 5);
                    break;


                // OK BORED NOW
                case "throw":
                    si(i, PrimaryVerb.Punt, 10, 1, 5, 1);
                    break;

                case "punt":
                    si(i, PrimaryVerb.Punt, 5, 1, 5, 10);
                    break;

                case "stop":
                    si(i, PrimaryVerb.Stop, 0, 2, 2, 1);
                    break;

                case "open":
                    si(i, PrimaryVerb.Open, 1, 1, 1, 1);
                    break;

                case "close":
                case "shut":
                    si(i, PrimaryVerb.Close, 1, 1, 1, 1);
                    break;

                case "climb up":
                case "climb":
                case "up":
                case "go up":
                    si(i, PrimaryVerb.Climb, 1, 1, 1, 1);
                    break;

                case "clamber up":
                case "clamber":
                    si(i, PrimaryVerb.Climb, 2, 1, 1, 5);
                    break;

                case "climb down":
                case "descend":
                case "down":
                case "go down":
                    si(i, PrimaryVerb.Descend, 1, 1, 1, 1);
                    break;

                case "clamber down":
                    si(i, PrimaryVerb.Descend, 2, 1, 1, 5);
                    break;

                case "modify":
                    si(i, PrimaryVerb.Modify, 1, 1, 1, 1);
                    break;

                case "repair":
                case "fix":
                    si(i, PrimaryVerb.Modify, 1, 5, 1, 1);
                    break;

                case "look at":
                case "look":
                case "examine":
                case "x":
                    si(i, PrimaryVerb.Look, 1, 1, 1, 1);
                    break;

                case "go":
                case "go north":
                case "go backward":
                case "forward":
                case "backward":
                case "left":
                case "right":
                case "north":
                case "east":
                case "south":
                case "west":
                case "go east":
                case "go south":
                case "go west":
                case "GOO WEEEEEEEEEESSSSTTTT":
                case "go forward":
                case "go left":
                case "go right":
                    si(i, PrimaryVerb.Go, 1, 1, 1, 1);

                default:
                    i.Verb = PrimaryVerb.DontKnow;
                    break;
            }

            return i;
        }
    }
}

