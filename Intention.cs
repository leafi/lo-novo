using System;

namespace lo_novo
{
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

        public Player InstigatorOrNull;
        public Thing TargetOrNull;
        public Thing WithOrNull;
    }
}
