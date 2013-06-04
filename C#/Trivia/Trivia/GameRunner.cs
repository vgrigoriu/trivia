using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        private static bool notAWinner;

        public static void Main(String[] args)
        {
            Game aGame = new Game(Console.Out);

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            Random rand = new Random(0);

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

