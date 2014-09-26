using System;

namespace lo_novo.LabRaid
{
    public class ProjectChamber : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "Project Chamber"; } }

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

        public ProjectChamber()
        {
            AddExit(Direction.NorthWest, typeof(Landing1), "landing (#)?1", "(my|our) ship");
            AddExit(Direction.NorthEast, typeof(Landing2), "landing (#)?2", "secret");
            AddExit(Direction.West, typeof(WestCorridorN));
            AddExit(Direction.East, typeof(EastCorridorN));
        }
    }
}

