using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class RandomAI : IPlayer
    {
        //Random play
        Random random = new Random();
        public int NextMove()
        {
            return random.Next(0, 3);
        }

        public void SaveResult(int myMove, int otherMove)
        {
        }
    }
}
