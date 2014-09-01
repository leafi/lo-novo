using System;

namespace lo_novo
{
    public class SalesmanControlRoomThing : Thing
    {
        public override string Name { get { return "salesman"; } }

        public bool FlashbackEnabled = true;
        public bool FlashbackDone = false;

        public SalesmanControlRoomThing()
        {
            AddAliases("(trusty )?rusty", "rusty's( trusty)?( slightly[ \\-]used)?( space ?mobile)?( emporium)?");
        }

        public bool DoSalesmanControlRoomThingThing()
        {
            if (!FlashbackEnabled)
                State.o("You have no time to consider the SALESMAN and his past antics at this point - more pressing matters are afoot.");
            else if (FlashbackDone)
                State.o("With the subtly placed and well-written flashback over, you wish only to purge the blasted SALESMAN from your mind.");
            else
            {
                State.ann("At " + State.Player.Name + "'s behest, you find yourselves dragged into a flashback. It was many moons ago...");
                State.TravelAll(typeof(SalesmanFlashbackRoom));
                FlashbackDone = true;
            }

            return true;
        }
    }

    public class SalesmanFlashbackRoom : Room
    {
        public override string Name { get { return "TRUSTY RUSTY'S SLIGHTLY-USED SPACEMOBILE EMPORIUM"; } }

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
                return @"The emporium. You're in it.
It smells of desperation, and of sacrificing anything for a quick buck.
Like a novelty Toblerone.";
            }
        }

        public SalesmanFlashbackRoom()
        {
        }

    }
}

