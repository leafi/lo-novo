using System;

namespace lo_novo.LabRaid
{
    public class DirectorsOffice : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "Director's Office"; } }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public DirectorsOffice()
        {
            AddExit(Direction.West, typeof(EastCorridorN), "corridor");
        }
    }
}

