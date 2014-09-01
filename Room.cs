using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public abstract class Room : FalseIObey, ITick, INoun
    {
        public bool Unvisited = true;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string ShortDescription { get; }
        public List<Thing> Contents = new List<Thing>();
        public Dictionary<Thing, string> DescriptionForContent = new Dictionary<Thing, string>();

        protected RoomParser parser;
        internal DefaultRoomResponses DefaultRoomResponses;

        public List<Tuple<string, EatsNoun, Func<Intention, bool>>> CustomRegex
            = new List<Tuple<string, EatsNoun, Func<Intention, bool>>>();

        private double timeToNextDescribeOnEntry = 0.0;

        public Thing Find(string name)
        {
            return Contents.Where<Thing>((t) => t.Name == name).Single();

        }

        public T Find<T>() where T : Thing
        {
            return Contents.Where<Thing>((t) => typeof(T) == t.GetType()).Cast<T>().Single();
        }

        public T Find<T>(string name) where T : Thing
        {
            return Contents.Where<Thing>((t) => typeof(T) == t.GetType()).Cast<T>()
                .Where<T>((p) => p.Name == name).Single();
        }

        public IEnumerable<Player> Players
        {
            get
            {
                return State.AllPlayers.Where<Player>((p) => p.Room == this);
            }
        }

        public Room()
        {
            BuildParser();
        }
        
        /// <summary>
        /// Announcement made to all players, irrespective of who triggered the command.
        /// </summary>
        /// <param name="output">Text to announce to all players</param>
        public void ann(string output)
        {
            State.AllIRC.Send(output);
        }

        /// <summary>
        /// Addresses the player who triggered this command, but publicly.
        /// </summary>
        /// <param name="output">Text to display (publicly!) to the player who triggered this command</param>
        public void o(string output)
        {
            State.Player.IRC.Send(output);
        }

        /// <summary>
        /// Addresses the player who triggered this command, privately.
        /// </summary>
        /// <param name="output">Text to whisper to the player who triggered this command</param>
        public void whis(string output)
        {
            State.Player.IRC.Send(output, true);
        }

        public virtual void Describe(bool toAll = false, bool longDesc = true)
        {
            Action<string> d = ((s) => { if (toAll) ann(s); else o(s); });

            d(Name.ToUpper());
            d(longDesc ? Description : ShortDescription);
            foreach (var t in Contents)
                if (t.Important)
                    d(t.InRoomDescription);
        }

        public virtual void Enter()
        {
            if (!State.Ticking.Contains(this))
                State.Ticking.Add(this);

            if (timeToNextDescribeOnEntry <= 0.0)
            {
                Describe(true, Unvisited);
            }
            else
                State.o("-> " + Name);

            timeToNextDescribeOnEntry = 30;
            Unvisited = false;
        }

        public virtual void Leave() 
        {
            if (Players.SingleOrDefault() == State.Player && State.Ticking.Contains(this))
                State.Ticking.Remove(this);
        }

        public virtual void Tick() 
        {
            timeToNextDescribeOnEntry -= State.DeltaTime;
        }

        protected virtual void BuildParser()
        {
            parser = new RoomParser(this);
            DefaultRoomResponses = new DefaultRoomResponses(this);
        }

        public virtual void Parse(string s)
        { 
            parser.Parse(s); 
        }

        public override string ToString()
        {
            return (State.Room == this) ? "the room" : Name;
        }

    }
}
