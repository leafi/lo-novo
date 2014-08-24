using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lo_novo
{
    class Program
    {
        //public const bool Safe = false;

        static void Main(string[] args)
        {
            ConsoleStateSetup.Setup();
            //IRCStateSetup.Setup();

            State.SystemMessage("Welcome, one and all. Let's start the game.\n");

            // move all players to init room
            var r = new Lobby();
            State.Room = r;
            foreach (var p in State.AllPlayers)
                p.Room = r;
            r.Start();


            var toTick = new List<ITick>();
            var lastTime = DateTime.UtcNow;

            while (true)
            {
                toTick.Clear();
                toTick.AddRange(State.Ticking);

                var newTime = DateTime.UtcNow;
                double dt = (newTime - lastTime).TotalSeconds;
                if (dt > 5.00)
                {
                    State.SystemMessage("Lagging, or time changed? " + dt.ToString() + "s passed, which is >5s");
                    dt = 5.00;
                }
                State.Time += dt;
                State.DeltaTime = dt;
                lastTime = newTime;

                foreach (var t in toTick)
                    t.Tick();

                foreach (var p in State.AllPlayers)
                {
                    State.Player = p;

                    while (true)
                    {
                        State.Intention = null;
                        State.Room = p.Room;

                        var s = p.IRC.TryRead();
                        if (s != null)
                            p.Room.Parse(s);
                        else
                            break;
                    }
                }

                Thread.Sleep(100);
            }
            

        }
    }
}
