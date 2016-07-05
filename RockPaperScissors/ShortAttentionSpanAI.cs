using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class ShortAttentionSpanAI : IPlayer
    {
        int saveResultNextMove;
        public int NextMove()
        {
            //Play the opposite of Player 2's last move
            if (saveResultNextMove == 0)
            {
                return 1;
            }
            else if (saveResultNextMove == 1)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public void SaveResult(int myMove, int otherMove)
        {
            saveResultNextMove = otherMove;
        }
    }
}
