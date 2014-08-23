﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public class MegaParser
    {
        List<MaybeParser> Parsers = new List<MaybeParser>();

        public MegaParser(IEnumerable<MaybeParser> parsers) { Parsers.AddRange(parsers); }

        public void Parse(string input, string defaultResponse = "I don't know how to do that.")
        {
            if (!TryParse(input))
                State.Player.IRC.Send(defaultResponse);
        }

        public bool TryParse(string input)
        {
            foreach (var p in Parsers)
                if (p.TryParse(input))
                    return true;

            return false;
        }
    }
}
