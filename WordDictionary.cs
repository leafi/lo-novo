using System;
using System.Collections.Generic;

namespace lo_novo
{
    public static class WordDictionary  // ... as opposed to the other kind
    {
        public static Dictionary<string, string> Dict;

        static WordDictionary()
        {
            Dict = new Dictionary<string, string>();
            Dict.Add("microl", "microl, n. A very short while.");
            Dict.Add("viewshield", "viewshield, n. A really big and cool screen made of unobtanium that lets you see outside, or inside, or computer graphics, unless it doesn't.");

        }
    }
}

