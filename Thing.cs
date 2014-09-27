using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public abstract class Thing : FalseIObey, ITick, INoun
    {
        public abstract string Name { get; }
        public string Preposition = "a";
        private string inRoomDescription = null;
        public string InRoomDescription 
        { 
            get { return inRoomDescription ?? "A " + Name + " lies on the floor."; }
            set { inRoomDescription = value; }
        }
        private string furtherInRoomDescription = null;
        public string FurtherInRoomDescription
        {
            get { return furtherInRoomDescription ?? InRoomDescription; }
            set { furtherInRoomDescription = value; }
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

        public void Tick() { }

        public override string ToString()
        {
            return Preposition + " " + Name;
        }
       
    }
}
