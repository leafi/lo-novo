using System;

namespace lo_novo
{
    public class BedRoom : Room
    {
        #region implemented abstract members of Room

        public override string Name { get { return "BEDR O'ER SUN"; } }

        public override string Description
        {
            get
            {
                return @"A dusty glaze coats your room, and the ancient red sun stares you dead-on. 
Safe but for the viewshield, you feel the warm light falling onto your skin. Tiny particles, visible for microls, walk between the strong rays coming from Big Ember.
Your room is a mess, but the ancient and terrible dance of the universe continues.";
            }
        }

        public override string ShortDescription
        {
            get
            {
                return @"Your room. Messy, but the view makes up for it.";
            }
        }

        #endregion

        public BedRoom()
        {
        }
    }
}

