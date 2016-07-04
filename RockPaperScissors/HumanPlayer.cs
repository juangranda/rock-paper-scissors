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
                Console.Write("Enter R, P, or S (Q to quit): ");
                string choice = Console.ReadLine().ToUpper();
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
                if (choice == "Q")
                {
                    return -1;
                }
                Console.WriteLine("I didn't understand that.");
            }
        }


        public int NextMove()
        {
            return getNextMove();
        }

        public void SaveResult(int myMove, int otherMove)
        {
            // do nothing
        }
    }
}
