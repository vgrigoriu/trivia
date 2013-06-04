using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class Players
    {
        private IList<Player> players;
        private IEnumerator<Player> playerEnumerator;

        public int Count 
        {
            get
            {
                return players.Count;
            }
        }

        public Player CurrentPlayer
        {
            get { return playerEnumerator.Current; }
        }

        public Players()
        {
            players = new List<Player>();
            playerEnumerator = PlayerEnumerator();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void AdvanceCurrentPlayer()
        {
            playerEnumerator.MoveNext();
        }

        private IEnumerator<Player> PlayerEnumerator()
        {
            while (true)
            {
                foreach (var player in players)
                {
                    yield return player;
                }
            }
        }
    }
}
