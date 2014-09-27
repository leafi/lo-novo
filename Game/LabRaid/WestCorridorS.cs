using System;

namespace lo_novo.LabRaid
{
    public class WestCorridorS : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "West Corridor (S)"; } }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public WestCorridorS()
        {
            AddExit(Direction.NorthWest, typeof(LivingQuartersB), "quarters");
            AddExit(Direction.North, typeof(WestCorridorN));
            AddExit(Direction.SouthWest, typeof(WestMaintenance), "maintenance", "cupboard", "closet");
            AddExit(Direction.East, typeof(ControlRoom), "control");
        }
    }
}

