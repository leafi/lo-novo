using System;

namespace lo_novo.LabRaid
{
    public class WestMaintenance : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "Maintenance Cupboard (W)"; } }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ShortDescription
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public WestMaintenance()
        {
            AddExit(Direction.East, typeof(WestCorridorS), "corridor");
        }
    }
}

