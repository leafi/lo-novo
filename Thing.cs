using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public abstract class Thing : FalseIObey, INoun
    {
        public abstract string Name { get; }
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

        public override string ToString()
        {
            return Preposition + " " + Name;
        }
       
    }
}
