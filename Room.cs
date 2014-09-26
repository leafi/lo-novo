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
        public virtual string[] Tags { get { return new string[] { }; } }
        public List<Thing> Contents = new List<Thing>();
        public Dictionary<Thing, string> DescriptionForContent = new Dictionary<Thing, string>();

        protected RoomParser parser;
        internal DefaultRoomResponses DefaultRoomResponses;

        public List<Tuple<string, EatsNoun, Func<Intention, bool>>> CustomRegex
            = new List<Tuple<string, EatsNoun, Func<Intention, bool>>>();

        // REGEX!!!
        public List<Tuple<string, Type>> Exits = new List<Tuple<string, Type>>();
        public List<string> ExitCanonicalNames = new List<string>();

        private double timeToNextDescribeOnEntry = 0.0;

        public enum Direction
        {
            None,
            North,
            East,
            South,
            West,
            NorthWest,
            NorthEast,
            SouthEast,
            SouthWest
        }

        public void AddExit(Direction direction, Type destination)
        {
            if (direction == Direction.None)
                throw new ArgumentException("Can't add exit with null direction without any aliases.");

            Exits.Add(Tuple.Create(Enum.GetName(typeof(Direction), direction).ToLowerInvariant(), destination));
            ExitCanonicalNames.Add(Enum.GetName(typeof(Direction), direction).ToUpper());
        }

        public void AddExit(Direction direction, Type destination, params string[] aliasesRegex)
        {
            if (direction == Direction.None)
                throw new ArgumentException("A canonical name must be provided for the exit, because direction is None.");

            var dn = Enum.GetName(typeof(Direction), direction);
            var ar = new string[] { dn }.Union(aliasesRegex);

            foreach (var r in ar)
                Exits.Add(Tuple.Create(r.ToLowerInvariant(), destination));
            ExitCanonicalNames.Add(dn.ToUpper());
        }

        public void AddExit(Direction direction, Type destination, bool changeCanonicalName, string canonicalName, params string[] aliasesRegex)
        {
            if (!changeCanonicalName) // fucking types
                throw new Exception();

            var add = (direction != Direction.None) ? new string[] { canonicalName, Enum.GetName(typeof(Direction), direction) } : new string[] { canonicalName };
            var ar = add.Union(aliasesRegex);

            foreach (var r in ar)
                Exits.Add(Tuple.Create(r.ToLowerInvariant(), destination));
            ExitCanonicalNames.Add(canonicalName.ToUpper());
        }

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
            try {
                d(longDesc ? Description : ShortDescription);
            } catch (NotImplementedException) {
                d("Description/ShortDescription: NOTIMPLEMENTEDEXCEPTION");
            }
            foreach (var t in Contents)
                if (t.Important)
                    d(t.InRoomDescription);
            if (Exits.Count > 0)
            {
                d("Exits lie to the " + string.Join(", ", ExitCanonicalNames) + ".");
            }
        }

        public virtual void Enter()
        {
            if (!State.Ticking.Contains(this))
                State.Ticking.Add(this);

            if (timeToNextDescribeOnEntry <= 0.0)
            {
                Describe(true, Unvisited);
            }
            else if (!State.TravellingAll)
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

        /// <summary>
        /// Postprocessing function on parse result, letting the room script tweak the Intention if it wishes.
        /// </summary>
        /// <returns>same or new Intention, possibly with changed fields</returns>
        /// <param name="intent">Intention before post-processing</param>
        /// <param name="s">original string before parsing</param>
        public virtual Intention ParsePostprocess(Intention intent, string s) 
        {
            return intent;
        }

        /// <summary>
        /// Chance for a room to handle a command before traditional dispatching.
        /// </summary>
        /// <param name="i">the Intention about to undergo traditional dispatch</param>
        /// <returns><c>true</c> if dispatch was handled in this function, else <c>false</c>.</returns>
        public virtual bool EarlyDispatch(Intention i)
        {
            return false;
        }

        public override string ToString()
        {
            return (State.Room == this) ? "the room" : Name;
        }

    }
}
