using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public abstract class MaybeParser
    {
        public abstract bool TryParse(string text);
    }
}
