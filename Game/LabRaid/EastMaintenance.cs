using System;

namespace lo_novo.LabRaid
{
    public class EastMaintenance : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "Maintenance Cupboard (E)"; } }

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

        public EastMaintenance()
        {
            AddExit(Direction.West, typeof(EastCorridorN), "corridor");
        }
    }
}

