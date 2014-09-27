using System;

namespace lo_novo.LabRaid
{
    public class EastCorridorS : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "East Corridor (S)"; } }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public EastCorridorS()
        {
            AddExit(Direction.North, typeof(EastCorridorN));
            AddExit(Direction.East, typeof(ShitGym), "gym", "gymnasium");
            AddExit(Direction.West, typeof(ControlRoom), "control room");
        }
    }
}

