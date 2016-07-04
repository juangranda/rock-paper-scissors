﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Smart_Bot : IPlayer
    {
        int smartBotNextMove;
        List<int> Memory = new List<int>();

        public int NextMove()
        {
            return smartBotNextMove;
        }

        public void SaveResult(int myMove, int otherMove)
        {
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
            if (count0 > 0 && count1 > 0)
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
            else if (count0 > count1 && count0 > count2)
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
