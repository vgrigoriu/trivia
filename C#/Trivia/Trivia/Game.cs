using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Trivia;

namespace UglyTrivia
{
    public class Game
    {
        List<Player> players = new List<Player>();

        int[] places = new int[6];
        int[] purses = new int[6];

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        private TextWriter writer;

        public Game(TextWriter writer)
        {
            this.writer = writer;

            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(createRockQuestion(i));
            }
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            var newPlayer = new Player(playerName);

            players.Add(newPlayer);

            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            writer.WriteLine(playerName + " was added");
            writer.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            writer.WriteLine(players[currentPlayer].Name + " is the current player");
            writer.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    writer.WriteLine(players[currentPlayer].Name + " is getting out of the penalty box");
                    places[currentPlayer] = places[currentPlayer] + roll;
                    if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                    writer.WriteLine(players[currentPlayer].Name
                            + "'s new location is "
                            + places[currentPlayer]);
                    writer.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    writer.WriteLine(players[currentPlayer].Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                places[currentPlayer] = places[currentPlayer] + roll;
                if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                writer.WriteLine(players[currentPlayer].Name
                        + "'s new location is "
                        + places[currentPlayer]);
                writer.WriteLine("The category is " + currentCategory());
                askQuestion();
            }

        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                writer.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (currentCategory() == "Science")
            {
                writer.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (currentCategory() == "Sports")
            {
                writer.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (currentCategory() == "Rock")
            {
                writer.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private String currentCategory()
        {
            if (places[currentPlayer] == 0) return "Pop";
            if (places[currentPlayer] == 4) return "Pop";
            if (places[currentPlayer] == 8) return "Pop";
            if (places[currentPlayer] == 1) return "Science";
            if (places[currentPlayer] == 5) return "Science";
            if (places[currentPlayer] == 9) return "Science";
            if (places[currentPlayer] == 2) return "Sports";
            if (places[currentPlayer] == 6) return "Sports";
            if (places[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    writer.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    writer.WriteLine(players[currentPlayer].Name
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                writer.WriteLine("Answer was corrent!!!!");
                purses[currentPlayer]++;
                writer.WriteLine(players[currentPlayer].Name
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            writer.WriteLine("Question was incorrectly answered");
            writer.WriteLine(players[currentPlayer].Name + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }

}
