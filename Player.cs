using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public class Player : INoun
    {
        public string Name;
        public List<string> Aliases = new List<string>();
        public IIRC IRC;
        public Room Room;
        public List<Thing> Inventory = new List<Thing>();
        public DefaultInventoryResponses DefaultInventoryResponses;

        public void Tick() { }

        public override string ToString()
        {
            return Name;
        }
    }
}
