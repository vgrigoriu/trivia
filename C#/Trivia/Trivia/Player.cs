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
        public bool IsInPenaltyBox { get; set; }
        public int Place { get; set; }  

        public Player(string name)
        {
            Name = name;
            Purse = 0;
            Place = 0;
            IsInPenaltyBox = false;
        }

        public void AddToPurse(int amount)
        {
            Purse += amount;
        }


    }
}
