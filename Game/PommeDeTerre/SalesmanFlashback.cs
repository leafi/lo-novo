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
                return (Unvisited ? @"You land in grainy black 'n' white, in cycles gone by, in the past.
Beady-eyed, sweaty men with little hair glance at you, but the eye-fucking by RUSTY hisself shows you that you're exactly one man's meal today.
As for the EMPORIUM itself, " : "Regarding the EMPORIUM, ") + @" it's some big-ass SPACE SHOWROOM with SHINY SHIPS and LESS SHINY SHIPS dotted around.
PROTO-MARBLE is what the floor is, the ceiling's too far up to make out against the SPOTLIGHTS on the SHINY SHIPS, and the air is stale like last year's Toblerone.
So many SALESMEN, yet you're the only one there. At risk of belabouring the point, they're sharks, and you're meat.";
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

