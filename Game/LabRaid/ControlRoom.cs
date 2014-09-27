using System;

namespace lo_novo.LabRaid
{
    public class ControlRoom : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "Control Room"; } }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public ControlRoom()
        {
            AddExit(Direction.West, typeof(WestCorridorS));
            AddExit(Direction.East, typeof(EastCorridorS));
        }
    }
}

