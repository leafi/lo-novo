using System;

namespace lo_novo.LabRaid
{
    public class EastCorridorN : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "East Corridor (N)"; } }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public EastCorridorN()
        {
            AddExit(Direction.West, typeof(ProjectChamber), "chamber");
            AddExit(Direction.NorthEast, typeof(DirectorsOffice), "office");
            AddExit(Direction.SouthEast, typeof(EastMaintenance), "maintenance", "cupboard", "closet");
            AddExit(Direction.South, typeof(EastCorridorS));
        }
    }
}

