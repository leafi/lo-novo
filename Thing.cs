using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public abstract class Thing : FalseIObey, ITick
    {
        public abstract string Name { get; }
        public string Preposition = "a";
        private string description = null;
        public string Description
        {
            get { return description ?? "Truly, a " + Name + " remarkable only in how unremarkable it is."; }
            set { description = value; }
        }
        private string shortDescription = null;
        public string ShortDescription 
        { 
            get { return shortDescription ?? Description; }
            set { shortDescription = value; }
        }
        public bool Heavy = true;
        public bool CanTake = false;

        public void Tick() { }

        public override string ToString()
        {
            return Preposition + " " + Name;
        }
       
    }
}
