using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        public static void graphic() // ASCII text art
        {
           Console.Write(
@"                               ___  ___  __   _    
                              | |_)/ / \/ /` | |_/ 
                              |_| \\_\_/\_\_,|_| \
                             ___  __   ___  ____ ___  
                            | |_)/ /\ | |_)| |_ | |_) 
                            |_| /_/--\|_|  |_|__|_| \
                     __   __    _   __   __   ___   ___   __  
                    ( (` / /`  | | ( (` ( (` / / \ | |_) ( (` 
                    _)_) \_\_, |_| _)_) _)_) \_\_/ |_| \ _)_) ");
        }

        public static Dictionary<string, IPlayer> AIPlayers = new Dictionary<string, IPlayer>()
        {
            // Add AIs by filling in lines like the ones below
            { "RANDOM BOT", new RandomAI() },
            { "STUBBORN BOT", new StubbornAI(1) },
            { "SHORT ATTENTION SPAN BOT", new ShortAttentionSpanAI() },
            { "SMART BOT", new Smart_Bot() },
        };

        static void Main(string[] args)
        {
            //if (AIPlayers.Count < 1)
            //{
            //    Console.WriteLine("No AI players exist!");
            //    Console.ReadKey();
            //    Environment.Exit(0);
            //}
            bool validChoice = false;
            while (!validChoice)
            {
                try
                {
                    //MAIN MENU
                    Console.ForegroundColor = ConsoleColor.Red;
                    graphic();
                    Console.ResetColor();
                    Console.WriteLine("\n\n\n\t\t\t\t1. HUMAN vs BOT");
                    Console.WriteLine("\n\t\t\t\t2. BOT vs BOT");
                    Console.WriteLine("\n\t\t\t\t3. QUIT");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n\t\t\tChoose a number from the menu");

                    Console.ResetColor();
                    Console.Write("\n\t\t\t\t> ");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    validChoice = true;
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    string s = "===========================================================================";
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.WriteLine();
                    Console.ResetColor();

                    //Execute menu choice
                    if (choice < 1 || choice > 3)
                    {
                        validChoice = false;
                        Console.WriteLine("Please enter a number between 1 and 3");
                    }
                    if (choice == 1)
                    {
                        HumanVsAI();
                        validChoice = false;
                    }
                    else if (choice == 2)
                    {
                        AIVsAI();
                        validChoice = false;
                    }
                    else if (choice == 3)
                    {
                        break;
                    }
                }
                catch 
                {
                    Console.WriteLine("Please type in a number using the number keys");
                }
            }
        }

        static string MoveToString(int choice) //Convert int choice to: Rock || paper || Scissors
        {
            if (choice == 0)
            {
                return "ROCK    ";
            }
            else if (choice == 1)
            {
                return "PAPER   ";
            }
            else if (choice == 2)
            {
                return "SCISSORS";
            }
            else
            {
                return "UNKNOWN";
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

            // Print play
            Console.ForegroundColor = ConsoleColor.Red;
            //Console.Write("Player 1: {0}\t\tPlayer 2: {1}", MoveToString(p1Choice), MoveToString(p2Choice));
            Console.Write("Player 1: {0}", MoveToString(p1Choice));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\tPlayer 2: {0}", MoveToString(p2Choice));
            Console.WriteLine();
            Console.ResetColor();

            int winner = CalculateWinner(p1Choice, p2Choice);

            //pass result on to IPlayer Save Result()
            player1.SaveResult(p1Choice, p2Choice);
            player2.SaveResult(p2Choice, p1Choice);

            return winner;
        }

        static string SelectAI()
        {
            Console.WriteLine("\nSelect an AI:\n");

            // Show each of the possible opponents
            for (int i = 0; i < AIPlayers.Keys.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, AIPlayers.Keys.ElementAt(i));
            }

            Console.Write("\n> ");
            int aiChoice = Convert.ToInt32(Console.ReadLine());
            return AIPlayers.Keys.ElementAt(aiChoice - 1);
        }

        static void HumanVsAI()
        {
            IPlayer human = new HumanPlayer();
            int humanWins = 0;
            int aiWins = 0;
            int ties = 0;

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("YOU Vs");
            Console.ResetColor();
        
            string chosenAI = SelectAI(); // show list of opponent options

            //Banner: Player1 vs Player2
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n YOU");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("    ▄▀▄▀▄▀ VS ▀▄▀▄▀▄    ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("{0}\n", chosenAI);
            Console.ResetColor();
            Console.WriteLine();

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

                //Print score
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nYOU: {0}      AI: {1}      Ties: {2}\n", humanWins, aiWins, ties);
                Console.ResetColor();
            }
        }

        static void AIVsAI()
        {
            // show list of opponent options for Bot1
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("BOT #1:");
            Console.ResetColor();
            string chosenAI1 = SelectAI();
            
            // show list of opponent options for Bot2
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nBOT #2:");
            Console.ResetColor();
            string chosenAI2 = SelectAI();

            int ai1Wins = 0;
            int ai2Wins = 0;
            int ties = 0;

            IPlayer ai1 = AIPlayers[chosenAI1];
            IPlayer ai2 = AIPlayers[chosenAI2];

            while (true)
            {
                //Banner: Player1 vs Player2
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n {0}", chosenAI1);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("    ▄▀▄▀▄▀ VS ▀▄▀▄▀▄    ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("{0}\n", chosenAI2);
                Console.ResetColor();

                //Enter # of matches
                Console.WriteLine("\nHow many matches?: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("(or press M for Main Menu)\n");
                Console.ResetColor();
                Console.Write("> ");
                string numberOfGamesString = Console.ReadLine().ToLower();
                Console.WriteLine();

                //Option to break loop and go back to Menu
                if (numberOfGamesString == "m")
                {
                    break;
                }

                int numberOfGames = Convert.ToInt32(numberOfGamesString); 

                //run # of games entered and keep score
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

                    //Print score
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n{0}: {1}      {2}: {3}      Ties: {4}\n", chosenAI1, ai1Wins, chosenAI2, ai2Wins, ties);
                    Console.ResetColor();
                }
            }
        }
    }
}
