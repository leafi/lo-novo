using System;
using System.Collections.Generic;
using System.Linq;

namespace lo_novo.PommeDeTerre
{
    public class ControlRoom : Room
    {
        public enum StateEnum
        {
            AInit,
            BConversation,
            CExplore
        }

        public StateEnum RoomState = StateEnum.AInit;

        public override string Name { get { return "POMME DE TERRE: CONTROL ROOM"; } }

        public override string Description
        {
            get
            {
                switch (RoomState)
                {
                    case StateEnum.AInit:
                        return @"You see a dark fog, punched through by the DIM RED HUE of the EMERGENCY LIGHTS.
Eventually, your eyes adjust, and you're able to perceive the room and its contents.
You're in the POMME DE TERRE - the APPLE OF THE EARTH, the ship of you and your COMPADRES, filled with the latest technology of about 50 cycles ago.
Well, the salesman was right about one thing - it was cheap.";

                    case StateEnum.BConversation:
                        return "dunno yet";

                    case StateEnum.CExplore:
                        return "dunno yet";
                }

                throw new NotImplementedException();
            }
        }

        public override string ShortDescription
        {
            get
            {
                switch (RoomState)
                {
                    case StateEnum.AInit:
                        return "Lit by a dim red hue, stretching out before you - but not much - is the control room of the Pomme de Terre, or Apple of the Earth.";

                    case StateEnum.BConversation:
                        return "dunno yet";

                    case StateEnum.CExplore:
                        return "dunno yet";
                }
                //return @"The control room of the ship. Not much going on here, 

                throw new NotImplementedException();
            }
        }

        public void Transition(StateEnum next)
        {
            if ((RoomState == StateEnum.AInit && next != StateEnum.BConversation)
                || (RoomState == StateEnum.BConversation && next != StateEnum.CExplore)
                || (RoomState == StateEnum.CExplore))
                throw new Exception(string.Format("incorrect room state transition. was {0} requested {1}", RoomState, next));

            RoomState = next;


        }

        public ControlRoom()
        {

        }
    }
}

