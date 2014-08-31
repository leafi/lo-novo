using System;

namespace lo_novo
{
    [SinglePlayer]
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
            Contents.Add(new ConceptThing("Mess",
                furtherInRoomDescription: "The mess is sprawling. You casually wonder how long it'll take before"
                + " it realises its own power and moves to take over the entire universe and everything in it."
                + "\nFrankly, it looks like it's about halfway there.",
                activate: "It's inert, thank the gods.",
                attack: "No effect. It's simply too strong.",
                pushPull: @"The problem with that is that there is no obvious place where the mess begins and where it ends.
You apply pressure to a close area, but it simply resumes its original shapeless shape once you stop.",
                talk: "No response. If alive, it is incapable of speech yet - or is lulling you into a false sense of security.",
                take: "You move to do so, but suddenly halt. Was that a growl?",
                punt: "Where would you even start?",
                stop: "No. It's gone too far already. Best to just let it happen.",
                climbDescend: @"No point scaling the thing without proper preparation.
You'd need a week's rations, proper climbing equipment and a guide native to the area, at the very least."
            ));

        }
    }
}

