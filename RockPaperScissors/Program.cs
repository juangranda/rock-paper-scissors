using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        public static Dictionary<string, IPlayer> AIPlayers = new Dictionary<string, IPlayer>()
        {
            // TODO:
            // Add AIs by filling in lines like the ones below
            { "Random 1", new RandomAI() },
            { "Stubborn 1", new StubbornAI(0) },

            { "Short Attention Span 1", new ShortAttentionSpanAI() },

           // { "Other", new OtherAI() },
           // { "YetAnother", new YetAnotherAI() },
        };

        static void Main(string[] args)
        {
            if (AIPlayers.Count < 1)
            {
                Console.WriteLine("No AI players exist!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            bool success = false;
            while (!success)
            {
                Console.WriteLine("\n1. Human vs AI");
                Console.WriteLine("2. AI vs AI");
                Console.WriteLine("3. Quit");
                Console.Write("> ");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        HumanVsAI();
                    }
                    else if (choice == 2)
                    {
                        AIVsAI();
                    }
                    else if (choice == 3)
                    {
                        break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        static string MoveToString(int choice)
        {
            if (choice == 0)
            {
                return "Rock";
            }
            else if (choice == 1)
            {
                return "Paper";
            }
            else if (choice == 2)
            {
                return "Scissors";
            }
            else
            {
                return "Unknown";
            }
        }


        static int[,] winnerMatrix = 
        {  // R  P  S p1 // p2
            { 0, 1, 2 }, // R
            { 2, 0, 1 }, // P
            { 1, 2, 0 }  // S
        };

        /// <summary>
        /// Calculates the winner of a RPS game
        /// </summary>
        /// <param name="p1Choice"></param>
        /// <param name="p2Choice"></param>
        /// <returns>0 for tie, 1 for p1, 2 for p2</returns>
        static int CalculateWinner(int p1Choice, int p2Choice)
        {
            return winnerMatrix[p2Choice, p1Choice];
        }

        static int RunGame(IPlayer player1, IPlayer player2)
        {
            int p1Choice = player1.NextMove();
            int p2Choice = player2.NextMove();

            if (p1Choice == -1 || p2Choice == -1)
            {
                return -1;
            }

            Console.WriteLine("{0}\t{1}", MoveToString(p1Choice), MoveToString(p2Choice));

            int winner = CalculateWinner(p1Choice, p2Choice);

            player1.SaveResult(p1Choice, p2Choice);
            player2.SaveResult(p2Choice, p1Choice);
            

            return winner;
        }
        
        static string SelectAI()
        {
            Console.WriteLine("Select an AI:");

            // Show each of the possible opponents
            for (int i = 0; i < AIPlayers.Keys.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, AIPlayers.Keys.ElementAt(i));
            }

            Console.Write("> ");
            int aiChoice = Convert.ToInt32(Console.ReadLine());
            return AIPlayers.Keys.ElementAt(aiChoice - 1);
        }

        static void HumanVsAI()
        {
            IPlayer human = new HumanPlayer();
            int humanWins = 0;
            int aiWins = 0;
            int ties = 0;

            string chosenAI = SelectAI();

            Console.WriteLine("\nPlaying against {0}...\n", chosenAI);
            IPlayer ai = AIPlayers[chosenAI];

            while (true)
            {
                int winner = RunGame(human, ai);
                if (winner == -1)
                {
                    break;
                }
                if (winner == 1)
                {
                    humanWins++;
                }
                else if (winner == 2)
                {
                    aiWins++;
                }
                else if (winner == 0)
                {
                    ties++;
                }

                Console.WriteLine("\nHuman: {0}\tAI: {1}\tTies: {2}\n", humanWins, aiWins, ties);
            }
        }

        static void AIVsAI()
        {
            string chosenAI1 = SelectAI();
            string chosenAI2 = SelectAI();

            Console.Write("How many games? ");
            int numberOfGames = Convert.ToInt32(Console.ReadLine());

            int ai1Wins = 0;
            int ai2Wins = 0;
            int ties = 0;

            IPlayer ai1 = AIPlayers[chosenAI1];
            IPlayer ai2 = AIPlayers[chosenAI2];

            for (int i = 0; i < numberOfGames; i++)
            {
                int winner = RunGame(ai1, ai2);
                if (winner == 1)
                {
                    ai1Wins++;
                }
                else if (winner == 2)
                {
                    ai2Wins++;
                }
                else if (winner == 0)
                {
                    ties++;
                }

                Console.WriteLine("{0}: {1}\t{2}: {3}\tTies: {4}\n", chosenAI1, ai1Wins, chosenAI2, ai2Wins, ties);

            }
        }

    }
}
