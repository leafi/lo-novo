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
        private string description = null;
        public string Description
        {
            get { return description ?? "Truly, a " + Name + " remarkable only in how unremarkable it is."; }
            set { description = value; }
        }
        private string inRoomDescription = null;
        public string InRoomDescription 
        { 
            get { return inRoomDescription ?? "A " + Name + " lies on the floor."; }
            set { inRoomDescription = value; }
        }
        public List<string> AliasesRegex = new List<string>();

        public bool Heavy = true;
        public bool CanTake = false;

        /// <summary>
        /// Should we tell the user about this object when describing the room?
        /// </summary>
        public bool Important = true;

        public void Tick() { }

        public override string ToString()
        {
            return Preposition + " " + Name;
        }
       
    }
}
