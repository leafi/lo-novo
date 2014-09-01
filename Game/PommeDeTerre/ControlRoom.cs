using System;
using System.Collections.Generic;
using System.Linq;

namespace lo_novo
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

        public int TurnCounter = 5;

        public override string Name { get { return "POMME DE TERRE: CONTROL ROOM"; } }

        public override string Description
        {
            get
            {
                switch (RoomState)
                {
                    case StateEnum.AInit:
                        return "dunno yet";

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
                        return "dunno yet";

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

