using System;

namespace lo_novo.LabRaid
{
    public class LivingQuartersB : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "Living Quarters B"; } }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public LivingQuartersB()
        {
            AddExit(Direction.East, typeof(WestCorridorS), "corridor");
        }
    }
}

