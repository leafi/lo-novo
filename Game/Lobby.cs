﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public class Lobby : Room
    {
        public override string Name
        {
            get { return "Lobby"; }
        }

        public override string Description
        {
            get { return "The room is dark and most unseemly. Surely not a place you'd want to be in for long - it's not lit by the glorious sunshine of being in a good place for good people. \n\nRemember - in multiplayer, you need to prefix commands to the game with a single >."; }
        }

        public override string ShortDescription
        {
            get { return "A room for chit-chattin' and organizin' before the game begins proper."; }
        }
    }
}
