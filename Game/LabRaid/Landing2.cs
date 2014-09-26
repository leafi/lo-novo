using System;

namespace lo_novo.LabRaid
{
    public class Landing2 : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "Landing #2"; } }

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

        public Landing2()
        {
            AddExit(Direction.South, typeof(ProjectChamber), "chamber", "project");
        }
    }
}

