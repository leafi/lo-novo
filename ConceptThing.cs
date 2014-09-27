using System;

namespace lo_novo
{
    public class ConceptThing : Thing
    {
        private string name;
        private string activate = null;
        private string attack = null;
        private string pushPull = null;
        private string talk = null;
        private string take = null;
        private string punt = null;
        private string stop = null;
        private string openClose = null;
        private string climbDescend = null;
        private string modify = null;

        public override string Name { get { return name; } }

        public ConceptThing(string name, //string description = null, string inRoomDescription = null,
            string furtherInRoomDescription = null, string activate = null, string attack = null,
            string pushPull = null, string talk = null, string take = null, string punt = null,
            string stop = null, string openClose = null, string climbDescend = null, string modify = null)
        {
            this.name = name;
            //this.Description = description;
            //this.InRoomDescription = inRoomDescription;
            this.Description = furtherInRoomDescription;
            this.activate = activate;
            this.attack = attack;
            this.pushPull = pushPull;
            this.talk = talk;
            this.take = take;
            this.punt = punt;
            this.stop = stop;
            this.openClose = openClose;
            this.climbDescend = climbDescend;
            this.modify = modify;

            this.Announce = false;
            this.CanTake = false;
            this.Heavy = true;
        }

        private bool magic(string outputOrNull)
        {
            if (outputOrNull != null)
            {
                State.o(outputOrNull);
                return true;
            }
            return false;
        }

        public override bool Activate(Intention i)
        {
            return magic(activate);
        }

        public override bool Climb(Intention i)
        {
            return magic(climbDescend ?? "Where would you even start?");
        }

        public override bool Descend(Intention i)
        {
            return magic(climbDescend);
        }

        public override bool Attack(Intention i)
        {
            return magic(attack);
        }

        public override bool Close(Intention i)
        {
            return magic(openClose);
        }

        public override bool Open(Intention i)
        {
            return magic(openClose);
        }

        public override bool Modify(Intention i)
        {
            return magic(modify ?? "It appears to be doing an adequate job as-is.");
        }

        public override bool Pull(Intention i)
        {
            return magic(pushPull);
        }

        public override bool Push(Intention i)
        {
            return magic(pushPull);
        }

        public override bool Punt(Intention i)
        {
            return magic(punt);
        }

        public override bool Take(Intention i)
        {
            return magic(take);
        }

        public override bool Stop(Intention i)
        {
            return magic(stop);
        }

        public override bool Talk(Intention i)
        {
            return magic(talk);
        }
    }
}

