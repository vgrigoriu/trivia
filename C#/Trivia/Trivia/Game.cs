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
        private Players players = new Players();

        QuestionCategory popCategory = new QuestionCategory("Pop");
        QuestionCategory scienceCategory = new QuestionCategory("Science");
        QuestionCategory rockCategory = new QuestionCategory("Rock");
        QuestionCategory sportsCategory = new QuestionCategory("Sports");

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

        public void StartGame()
        {
            players.AdvanceCurrentPlayer();
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            var newPlayer = new Player(playerName);

            players.AddPlayer(newPlayer);

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
            writer.WriteLine(players.CurrentPlayer.Name + " is the current player");
            writer.WriteLine("They have rolled a " + roll);

            if (players.CurrentPlayer.IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    writer.WriteLine(players.CurrentPlayer.Name + " is getting out of the penalty box");
                    AdvancePlayer(roll);

                    writer.WriteLine(players.CurrentPlayer.Name
                            + "'s new location is "
                            + players.CurrentPlayer.Place);
                    writer.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    writer.WriteLine(players.CurrentPlayer.Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                AdvancePlayer(roll);

                writer.WriteLine(players.CurrentPlayer.Name
                        + "'s new location is "
                        + players.CurrentPlayer.Place);
                writer.WriteLine("The category is " + currentCategory());
                askQuestion();
            }

        }

        private void AdvancePlayer(int steps)
        {
            players.CurrentPlayer.Place += steps;

            if (players.CurrentPlayer.Place > 11)
                players.CurrentPlayer.Place -= 12;
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
            if (players.CurrentPlayer.Place == 0) return "Pop";
            if (players.CurrentPlayer.Place == 4) return "Pop";
            if (players.CurrentPlayer.Place == 8) return "Pop";
            if (players.CurrentPlayer.Place == 1) return "Science";
            if (players.CurrentPlayer.Place == 5) return "Science";
            if (players.CurrentPlayer.Place == 9) return "Science";
            if (players.CurrentPlayer.Place == 2) return "Sports";
            if (players.CurrentPlayer.Place == 6) return "Sports";
            if (players.CurrentPlayer.Place == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (players.CurrentPlayer.IsInPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    writer.WriteLine("Answer was correct!!!!");
                    players.CurrentPlayer.AddToPurse(1);
                    writer.WriteLine(players.CurrentPlayer.Name
                            + " now has "
                            + players.CurrentPlayer.Purse
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
                players.CurrentPlayer.AddToPurse(1);
                writer.WriteLine(players.CurrentPlayer.Name
                        + " now has "
                        + players.CurrentPlayer.Purse
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                GoToNextPlayer();

                return winner;
            }
        }

        private void GoToNextPlayer()
        {
            players.AdvanceCurrentPlayer();
        }

        public bool wrongAnswer()
        {
            writer.WriteLine("Question was incorrectly answered");
            writer.WriteLine(players.CurrentPlayer.Name + " was sent to the penalty box");
            players.CurrentPlayer.IsInPenaltyBox = true;

            GoToNextPlayer();
            return true;
        }


        private bool didPlayerWin()
        {
            return !(players.CurrentPlayer.Purse == 6);
        }
    }

}
