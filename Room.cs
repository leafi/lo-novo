using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public abstract class Room : FalseIObey, ITick
    {
        public bool Unvisited = true;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string ShortDescription { get; }
        public List<Thing> Contents = new List<Thing>();

        protected MegaParser parser;

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

        public void Describe(bool toAll = false, bool longDesc = true)
        {
            Action<string> d = ((s) => { if (toAll) ann(s); else o(s); });

            d(longDesc ? Description : ShortDescription);
            foreach (var t in Contents)
                d(t.Description);
        }

        public void Start()
        {
            Describe(Unvisited, Unvisited);
            Unvisited = false;
        }

        public void Tick() { }

        protected void BuildParser()
        {
            parser = new MegaParser();
        }

        public void Parse(string s)
        { 
            parser.Parse(s); 
        }

        public override string ToString()
        {
            return (State.Room == this) ? "the room" : Name;
        }

    }
}
