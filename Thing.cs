using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public abstract class Thing : INoun, IHandleDispatch
    {
        public string Name = "NOT SET";
        public string Preposition = "a";
        public string QuickDescription = null;
        public string Description = null;

        public List<string> AliasesRegex = new List<string>();

        public bool Heavy = true;
        public bool CanTake = false;

        /// <summary>
        /// Should we tell the user about this object when describing the room?
        /// </summary>
        public bool Announce = true;

        public void AddAliases(params string[] aliasesRegex)
        {
            AliasesRegex.AddRange(aliasesRegex);
        }

        public virtual bool Dispatch(Intention i)
        {
            if (i.DefaultVerb == DefaultVerb.Look)
            {
                State.o(Description ?? "ENODESCRIPTION");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Chance for Thing to do something in response to an action occurring involving it.
        /// </summary>
        /// <returns><c>true</c> if we did something that means the game should not adlib about what happened to the Thing, <c>false</c> otherwise.</returns>
        /// <param name="i">The index.</param>
        public virtual bool PostDispatch(Intention i)
        {
            return false;
        }

        public override string ToString()
        {
            return Preposition + " " + Name;
        }
       
    }
}
