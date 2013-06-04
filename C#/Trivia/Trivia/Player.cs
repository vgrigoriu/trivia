using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class Player
    {
        public string Name { get; private set; }
        public int Purse { get; private set; }

        public Player(string name)
        {
            Name = name;
            Purse = 0;
        }

        public void AddToPurse(int amount)
        {
            Purse += amount;
        }
    }
}
