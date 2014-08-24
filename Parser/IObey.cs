using System;

namespace lo_novo
{
    public interface IObey
    {
        bool Activate(Intention i);
        bool Attack(Intention i);
        bool Push(Intention i);
        bool Pull(Intention i);
        bool Talk(Intention i);
        bool Take(Intention i);
        bool Punt(Intention i);
        bool Stop(Intention i);
        bool Open(Intention i);
        bool Close(Intention i);
        bool Climb(Intention i);
        bool Descend(Intention i);
        bool Modify(Intention i);
        bool Look(Intention i);
        bool DontKnow(Intention i);
    }
}
