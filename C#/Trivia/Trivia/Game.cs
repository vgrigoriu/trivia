using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using Trivia;

namespace UglyTrivia
{
    public class Game
    {
        List<Player> players = new List<Player>();

        QuestionCategory popCategory = new QuestionCategory("Pop");
        QuestionCategory scienceCategory = new QuestionCategory("Science");
        QuestionCategory rockCategory = new QuestionCategory("Rock");
        QuestionCategory sportsCategory = new QuestionCategory("Sports");

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        private TextWriter writer;

        public Game(TextWriter writer)
        {
            this.writer = writer;

            popCategory.CreateQuestions();
            scienceCategory.CreateQuestions();
            rockCategory.CreateQuestions();
            sportsCategory.CreateQuestions();
        }

        private static string createSportsQuestion(int i)
        {
            return "Sports Question " + i;
        }

        private static string createScienceQuestion(int i)
        {
            return "Science Question " + i;
        }

        private static string createPopQuestion(int i)
        {
            return "Pop Question " + i;
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

            if (players[currentPlayer].IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    writer.WriteLine(players[currentPlayer].Name + " is getting out of the penalty box");
                    AdvancePlayer(roll);

                    writer.WriteLine(players[currentPlayer].Name
                            + "'s new location is "
                            + players[currentPlayer].Place);
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

                AdvancePlayer(roll);

                writer.WriteLine(players[currentPlayer].Name
                        + "'s new location is "
                        + players[currentPlayer].Place);
                writer.WriteLine("The category is " + currentCategory());
                askQuestion();
            }

        }

        private void AdvancePlayer(int steps)
        {
            players[currentPlayer].Place += steps;

            if (players[currentPlayer].Place > 11)
                players[currentPlayer].Place -= 12;
        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                writer.WriteLine(popCategory.GetNextQuestion());
            }
            if (currentCategory() == "Science")
            {
                writer.WriteLine(scienceCategory.GetNextQuestion());
            }
            if (currentCategory() == "Sports")
            {
                writer.WriteLine(sportsCategory.GetNextQuestion());
            }
            if (currentCategory() == "Rock")
            {
                writer.WriteLine(rockCategory.GetNextQuestion());
            }
        }


        private String currentCategory()
        {
            if (players[currentPlayer].Place == 0) return "Pop";
            if (players[currentPlayer].Place == 4) return "Pop";
            if (players[currentPlayer].Place == 8) return "Pop";
            if (players[currentPlayer].Place == 1) return "Science";
            if (players[currentPlayer].Place == 5) return "Science";
            if (players[currentPlayer].Place == 9) return "Science";
            if (players[currentPlayer].Place == 2) return "Sports";
            if (players[currentPlayer].Place == 6) return "Sports";
            if (players[currentPlayer].Place == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (players[currentPlayer].IsInPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    writer.WriteLine("Answer was correct!!!!");
                    players[currentPlayer].AddToPurse(1);
                    writer.WriteLine(players[currentPlayer].Name
                            + " now has "
                            + players[currentPlayer].Purse
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    GoToNextPlayer();

                    return winner;
                }
                else
                {
                    GoToNextPlayer();
                    return true;
                }



            }
            else
            {

                writer.WriteLine("Answer was corrent!!!!");
                players[currentPlayer].AddToPurse(1);
                writer.WriteLine(players[currentPlayer].Name
                        + " now has "
                        + players[currentPlayer].Purse
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                GoToNextPlayer();

                return winner;
            }
        }

        private void GoToNextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
        }

        public bool wrongAnswer()
        {
            writer.WriteLine("Question was incorrectly answered");
            writer.WriteLine(players[currentPlayer].Name + " was sent to the penalty box");
            players[currentPlayer].IsInPenaltyBox = true;

            GoToNextPlayer();
            return true;
        }


        private bool didPlayerWin()
        {
            return !(players[currentPlayer].Purse == 6);
        }
    }

}
