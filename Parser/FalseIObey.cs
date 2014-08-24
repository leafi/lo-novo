using System;

namespace lo_novo
{
    public abstract class FalseIObey : IObey
    {
        #region IObey implementation

        public bool Activate(Intention i) { return false; }
        public bool Attack(Intention i) { return false; }
        public bool Push(Intention i) { return false; }
        public bool Pull(Intention i) { return false; }
        public bool Talk(Intention i) { return false; }
        public bool Take(Intention i) { return false; }
        public bool Punt(Intention i) { return false; }
        public bool Stop(Intention i) { return false; }
        public bool Open(Intention i) { return false; }
        public bool Close(Intention i) { return false; }
        public bool Climb(Intention i) { return false; }
        public bool Descend(Intention i) { return false; }
        public bool Modify(Intention i) { return false; }
        public bool Look(Intention i) { return false; }
        public bool DontKnow(Intention i) { return false; }

        #endregion
    }
}

