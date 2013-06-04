﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Trivia.EndToEndTests
{
    public class RunGameWithSeed
    {
        [Fact]
        public void RunGameWithSpecificSeed()
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);

            var gameRunner = new GameRunner();
            gameRunner.RunGame(writer, 0);

            var result = builder.ToString();

            result.Should().Be(expectedOutput);
        }

        private string expectedOutput = @"Chet was added
They are player number 1
Pat was added
They are player number 2
Sue was added
They are player number 3
Chet is the current player
They have rolled a 4
Chet's new location is 4
The category is Pop
Pop Question 0
Question was incorrectly answered
Chet was sent to the penalty box
Pat is the current player
They have rolled a 4
Pat's new location is 4
The category is Pop
Pop Question 1
Answer was corrent!!!!
Pat now has 1 Gold Coins.
Sue is the current player
They have rolled a 2
Sue's new location is 2
The category is Sports
Sports Question 0
Answer was corrent!!!!
Sue now has 1 Gold Coins.
Chet is the current player
They have rolled a 5
Chet is getting out of the penalty box
Chet's new location is 9
The category is Science
Science Question 0
Answer was correct!!!!
Chet now has 1 Gold Coins.
Pat is the current player
They have rolled a 5
Pat's new location is 9
The category is Science
Science Question 1
Answer was corrent!!!!
Pat now has 2 Gold Coins.
Sue is the current player
They have rolled a 2
Sue's new location is 4
The category is Pop
Pop Question 2
Answer was corrent!!!!
Sue now has 2 Gold Coins.
Chet is the current player
They have rolled a 4
Chet is not getting out of the penalty box
Pat is the current player
They have rolled a 5
Pat's new location is 2
The category is Sports
Sports Question 1
Answer was corrent!!!!
Pat now has 3 Gold Coins.
Sue is the current player
They have rolled a 5
Sue's new location is 9
The category is Science
Science Question 2
Answer was corrent!!!!
Sue now has 3 Gold Coins.
Chet is the current player
They have rolled a 4
Chet is not getting out of the penalty box
Pat is the current player
They have rolled a 5
Pat's new location is 7
The category is Rock
Rock Question 0
Question was incorrectly answered
Pat was sent to the penalty box
Sue is the current player
They have rolled a 5
Sue's new location is 2
The category is Sports
Sports Question 2
Answer was corrent!!!!
Sue now has 4 Gold Coins.
Chet is the current player
They have rolled a 4
Chet is not getting out of the penalty box
Pat is the current player
They have rolled a 5
Pat is getting out of the penalty box
Pat's new location is 0
The category is Pop
Pop Question 3
Answer was correct!!!!
Pat now has 4 Gold Coins.
Sue is the current player
They have rolled a 3
Sue's new location is 5
The category is Science
Science Question 3
Answer was corrent!!!!
Sue now has 5 Gold Coins.
Chet is the current player
They have rolled a 1
Chet is getting out of the penalty box
Chet's new location is 10
The category is Sports
Sports Question 3
Answer was correct!!!!
Chet now has 2 Gold Coins.
Pat is the current player
They have rolled a 2
Pat is not getting out of the penalty box
Sue is the current player
They have rolled a 4
Sue's new location is 9
The category is Science
Science Question 4
Answer was corrent!!!!
Sue now has 6 Gold Coins.
";
    }
}
