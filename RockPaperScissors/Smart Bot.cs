using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Smart_Bot : IPlayer
    {
        int smartBotNextMove;
        public static List<int> Memory = new List<int>();

        public int NextMove()
        {
            return smartBotNextMove;
        }

        public void SaveResult(int myMove, int otherMove)
        {
            //keep track of all player 2's moves
            Memory.Add(otherMove);

            int count0 = 0;
            int count1 = 0;
            int count2 = 0;
            for (int i = 0; i < Memory.Count; i++)
            {
                if (Memory[i] == 0)
                {
                    count0 ++;
                }
                if (Memory[i] == 1)
                {
                    count1++;
                }
                if (Memory[i] == 2)
                {
                    count2++;
                }
            }
            
            // Check for Short Attention Span Bot
            if (myMove == 0 && otherMove == 1)
            {
                smartBotNextMove = 2;
            }
            if (myMove == 1 && otherMove == 2)
            {
                smartBotNextMove = 0;
            }
            if (myMove == 2 && otherMove == 0)
            {
                smartBotNextMove = 1;
            }

            // Check for repetitive input from user
            if (otherMove == (myMove -1))
            {
                smartBotNextMove = 0;
            }

            //Play based on player 2's history
            else
            {
                if (count0 > 0 || count1 > 0)
                {
                    if (count0 == count1 || count1 == count2 || count2 == count0)
                    {
                        smartBotNextMove = myMove;
                        if (myMove == 0)
                        {
                            smartBotNextMove = 2;
                        }
                        else if (myMove == 1)
                        {
                            smartBotNextMove = 0;
                        }
                        else if (myMove == 2)
                        {
                            smartBotNextMove = 1;
                        }
                    }
                }

                //Play the opposite of Player 2's favorite move
                if (count0 > count1 && count0 > count2)
                {
                    smartBotNextMove = 1;
                }
                else if (count1 > count0 && count1 > count2)
                {
                    smartBotNextMove = 2;
                }
                else if (count2 > count0 && count2 > count1)
                {
                    smartBotNextMove = 0;
                }
            }
        }      
    }
}
