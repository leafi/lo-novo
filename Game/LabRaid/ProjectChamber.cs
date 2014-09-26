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
        }
    }
}

