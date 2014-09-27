using System;
using System.Collections.Generic;

namespace lo_novo
{
    public class ConceptThing : Thing
    {
        private string name;

        private Dictionary<DefaultVerb, FunOrString> actions
            = new Dictionary<DefaultVerb, FunOrString>();

        public override string Name { get { return name; } }

        public void Set(DefaultVerb verb, FunOrString funStr)
        {
            if (actions.ContainsKey(verb))
                actions.Remove(verb);

            actions.Add(verb, funStr);
        }

        public FunOrString Get(DefaultVerb verb)
        {
            return actions.ContainsKey(verb) ? actions[verb] : null;
        }

        public ConceptThing(string name, string description = null,
            FunOrString activate = null, FunOrString attack = null,
            FunOrString pushPull = null, FunOrString talk = null, FunOrString take = null, 
            FunOrString punt = null, FunOrString stop = null, FunOrString openClose = null, 
            FunOrString climbDescend = null, FunOrString modify = null)
        {
            this.name = name;
            this.Description = description;

            if (activate != null)
                Set(DefaultVerb.Activate, activate);
            if (attack != null)
                Set(DefaultVerb.Attack, attack);
            if (pushPull != null)
            {
                Set(DefaultVerb.Push, pushPull);
                Set(DefaultVerb.Pull, pushPull);
            }
            if (talk != null)
                Set(DefaultVerb.Talk, talk);
            if (take != null)
                Set(DefaultVerb.Take, take);
            if (punt != null)
                Set(DefaultVerb.Punt, punt);
            if (stop != null)
                Set(DefaultVerb.Stop, stop);
            if (openClose != null)
            {
                Set(DefaultVerb.Open, openClose);
                Set(DefaultVerb.Close, openClose);
            }
            if (climbDescend != null)
            {
                Set(DefaultVerb.Climb, climbDescend);
                Set(DefaultVerb.Descend, climbDescend);
            }
            if (modify != null)
                Set(DefaultVerb.Modify, modify);
        }

        private bool magic(string outputOrNull)
        {
            if (outputOrNull != null)
            {
                State.o(outputOrNull);
                return true;
            }
            return false;
        }

        public override bool Dispatch(Intention i)
        {
            if (actions.ContainsKey(i.DefaultVerb))
            {
                var a = actions[i.DefaultVerb];

                if (a.IsString)
                    State.o(a);
                else if (((Func<Intention, bool>)a)(i))
                    return true;
            }

            return base.Dispatch(i);
        }
    }
}

