using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class HumanPlayer : IPlayer
    {
        int getNextMove()
        {
            while (true)
            {
                //Enter play
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Enter R (rock), P (paper), or S (scissors):\n(M - Main Menu)");
                Console.ResetColor();
                Console.Write("\n>");
                string choice = Console.ReadLine().ToUpper();
                Console.WriteLine();

                //Convert play to int
                if (choice == "R")
                {
                    return 0;
                }
                if (choice == "P")
                {
                    return 1;
                }
                if (choice == "S")
                {
                    return 2;
                }
                if (choice == "M")
                {
                    return -1;
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("I didn't understand that.\n");
                Console.ResetColor();
            }
        }

        public int NextMove()
        {
            return getNextMove();
        }

        public void SaveResult(int myMove, int otherMove)
        {
        }
    }
}
