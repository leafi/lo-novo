using System;

namespace lo_novo
{
    public class DefaultRoomResponses : FalseIObey
    {
        protected Room room;

        public DefaultRoomResponses(Room r)
        {
            room = r;
        }

        public override bool Look(Intention i)
        {
            if (i.WholeRoom)
            {
                State.o(room.GetFullDescription());
                if (i.PassiveNoun != null)
                    State.o("You didn't find " + i.PassiveNoun.ToString() + " particularly helpful.");
                return true;
            }
            else if (i.ActiveNoun is Thing && room.Contents.Contains(i.ActiveNoun as Thing))
            {
                State.o((i.ActiveNoun as Thing).Description);
                return true;
            }
            else if (i.ActiveNoun is Player)
            {
                State.o((i.ActiveNoun as Player).Name + " sits there, inert. What the hell are they doing?");
                return true;
            }

            return false;
        }
    }
}

