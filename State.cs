using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    // Who triggered this command? and other fun questions
    public static class State
    {
        // TODO: make items that aren't the canonical source of information read-only to room scripts
        // probs backing var + prop { get { ... } }, no set {}, manual hidden set method elsewhere

        public static Room Room; // note: players *can* be in different rooms. and changing this doesn't change what room the player's in!
                                 // for everything else this class is maybe sorta the canonical source of information
        public static Player Player;
        public static Dictionary<string, Player> NameToPlayer = new Dictionary<string, Player>();
        public static List<Player> AllPlayers = new List<Player>();
        public static IIRC AllIRC;
        public static List<ITick> Ticking = new List<ITick>();

        public static Random RNG = new Random();

        public static Intention Intention = null;

        /// <summary>
        /// global game time in seconds
        /// </summary>
        public static double Time;

        /// <summary>
        /// time passed since last Tick() call in seconds
        /// </summary>
        public static double DeltaTime;

        public static void SystemMessage(string msg)
        {
            AllIRC.Send("SYSTEM: " + msg);
        }

        public static T FindRoom<T>() where T : class
        {
            foreach (var p in AllPlayers)
                if (p.Room is T)
                    return p.Room as T;

            return null;
        }

        public static IEnumerable<Room> GetOccupiedRooms()
        {
            return AllPlayers.ConvertAll((p) => p.Room).Distinct();
        }

        public static Room GetRoomContainingThing(Thing t)
        {
            if (State.Room != null && State.Room.Contents.Contains(t))
                return State.Room;

            return GetOccupiedRooms().TakeWhile((r) => r.Contents.Contains(t)).Single();
        }

        public static Player GetPlayerContainingThing(Thing t)
        {
            if (State.Player != null && State.Player.Inventory.Contains(t))
                return State.Player;

            return AllPlayers.TakeWhile((p) => p.Inventory.Contains(t)).Single();
        }

        public static INoun GetContainerOfThing(Thing t)
        {
            if (State.Player != null && State.Player.Inventory.Contains(t))
                return State.Player;

            return ((INoun) GetRoomContainingThing(t)) ?? (INoun) GetPlayerContainingThing(t);
        }

        /// <summary>
        /// Announcement made to all players, irrespective of who triggered the command.
        /// </summary>
        /// <param name="output">Text to announce to all players</param>
        public static void ann(string output)
        {
            State.AllIRC.Send(output);
        }

        /// <summary>
        /// Addresses the player who triggered this command, but publicly.
        /// </summary>
        /// <param name="output">Text to display (publicly!) to the player who triggered this command</param>
        public static void o(string output)
        {
            State.Player.IRC.Send(output);
        }

        /// <summary>
        /// Addresses the player who triggered this command, privately.
        /// </summary>
        /// <param name="output">Text to whisper to the player who triggered this command</param>
        public static void whis(string output)
        {
            State.Player.IRC.Send(output, true);
        }

        public static void RebuildNameToPlayer()
        {
            NameToPlayer.Clear();

            // don't let other players have an alias that's equal to someone's name
            // TODO: do the checking at add time, not here! (or just don't check at all..)
            IEnumerable<string> allNames = AllPlayers.ConvertAll<string>((p) => p.Name);
            foreach (var p in AllPlayers)
                foreach (var conflict in p.Aliases.Intersect(allNames))
                {
                    SystemMessage(p.Name + ", one of your aliases is " + conflict + "'s name. This alias will be discarded.");
                    p.Aliases.Remove(conflict);
                }

            foreach (var p in AllPlayers)
                foreach (var s in p.Aliases.Union(new string[] { p.Name }))
                    if (!NameToPlayer.ContainsKey(s))
                        NameToPlayer.Add(s, p);
                    else
                    {
                        SystemMessage(p.Name + ", the alias '" + s + "' is already in use by " + NameToPlayer[s].Name + ". I'm discarding it. Sorry.");
                        p.Aliases.Remove(s);
                    }
                        
        }
    }
}
