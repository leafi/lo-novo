﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public interface IIRC
    {
        string TryRead();
        void Send(string s, bool whisper = false);
    }
}
