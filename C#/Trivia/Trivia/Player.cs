using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class Player
    {
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}
