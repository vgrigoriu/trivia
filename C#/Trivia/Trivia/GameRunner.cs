using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        private bool notAWinner;

        public static void Main(String[] args)
        {
            var gameRunner = new GameRunner();

            gameRunner.RunGame(Console.Out, 0);
        }

        public void RunGame(TextWriter writer, int seed)
        {
            Game aGame = new Game(writer);

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            aGame.StartGame();

            Random rand = new Random(seed);

            do
            {
                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }
            } while (notAWinner);
        }
    }

}

