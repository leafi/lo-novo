using System;
using System.Text.RegularExpressions;

namespace lo_novo
{
    public class Intention
    {
        // all the following stats fields basically have 4 possible values:
        // 0, 1, 5 or 10. 0 is 'not at all', 1 is 'not especially', 10 is 'very much so'
        public int Violence = 1;
        public int Airborne = 1;
        public int Whimsy = 1;

        public DefaultVerb DefaultVerb;
        public string VerbString;

        public INoun ActiveNoun;
        public string ActiveNounString;
        public INoun PassiveNoun;
        public string PassiveNounString;

        public MatchCollection RegexMatches;

        public bool DispatchOnIObey(IObey iob)
        {
            switch (DefaultVerb)
            {
                case DefaultVerb.Activate:
                    return iob.Activate(this);

                case DefaultVerb.Attack:
                    return iob.Attack(this);

                case DefaultVerb.Climb:
                    return iob.Climb(this);

                case DefaultVerb.Close:
                    return iob.Close(this);

                case DefaultVerb.Descend:
                    return iob.Descend(this);

                case DefaultVerb.Look:
                    return iob.Look(this);

                case DefaultVerb.Modify:
                    return iob.Modify(this);

                case DefaultVerb.Open:
                    return iob.Open(this);

                case DefaultVerb.Pull:
                    return iob.Pull(this);

                case DefaultVerb.Punt:
                    return iob.Punt(this);

                case DefaultVerb.Push:
                    return iob.Push(this);

                case DefaultVerb.Stop:
                    return iob.Stop(this);

                case DefaultVerb.Take:
                    return iob.Take(this);

                case DefaultVerb.Talk:
                    return iob.Talk(this);
            }

            return false;
        }
    }
}
