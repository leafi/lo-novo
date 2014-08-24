using System;
using System.Collections.Generic;

namespace lo_novo
{
    public static class NounLibrary
    {
        /// <summary>
        /// Builds noun library using current state.
        /// </summary>
        public static IEnumerable<Tuple<string, INoun>> Build()
        {
            return new Tuple<string, INoun>[] { };
        }
    }
}

