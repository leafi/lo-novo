using System;

namespace lo_novo
{
    public abstract class FalseIObey : IObey
    {
        #region IObey implementation

        public virtual bool Activate(Intention i) { return false; }
        public virtual bool Attack(Intention i) { return false; }
        public virtual bool Push(Intention i) { return false; }
        public virtual bool Pull(Intention i) { return false; }
        public virtual bool Talk(Intention i) { return false; }
        public virtual bool Take(Intention i) { return false; }
        public virtual bool Punt(Intention i) { return false; }
        public virtual bool Stop(Intention i) { return false; }
        public virtual bool Open(Intention i) { return false; }
        public virtual bool Close(Intention i) { return false; }
        public virtual bool Climb(Intention i) { return false; }
        public virtual bool Descend(Intention i) { return false; }
        public virtual bool Modify(Intention i) { return false; }
        public virtual bool Look(Intention i) { return false; }
        public virtual bool DontKnow(Intention i) { return false; }

        #endregion
    }
}

