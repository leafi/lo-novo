using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo.LabRaid
{
    public class ShitGym : Room
    {
        public override string Name { get { return "Shit Gymnasium"; } }

        public override string Description
        {
            get { throw new NotImplementedException(); }
        }

        public ShitGym()
        {
            AddExit(Direction.West, typeof(EastCorridorS), "corridor");
        }
    }
}
