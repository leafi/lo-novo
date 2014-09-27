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
        private string quickDescription = null;
        public string QuickDescription 
        { 
            get { return quickDescription; }
            set { quickDescription = value; }
        }
        private string description = null;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public List<string> AliasesRegex = new List<string>();

        public bool Heavy = true;
        public bool CanTake = false;

        /// <summary>
        /// Should we tell the user about this object when describing the room?
        /// </summary>
        public bool Important = true;

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
