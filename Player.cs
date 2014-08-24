using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public class Player
    {
        public string Name;
        public List<string> Aliases = new List<string>();
        public IIRC IRC;
        public Room Room;

        public void Tick() { }

        public override string ToString()
        {
            return Name;
        }
    }
}
