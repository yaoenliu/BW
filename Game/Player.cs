using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    internal class Player
    {
        public Player()
        {
            int Frame = -1;
            int Direction = -1;
        }

        public void SetFrame(int handState)
        {
            Frame = handState;
        }

        public void SetDirection(int handDir)
        {
            Direction = handDir;
        }

        public int GetHand()
        {
            return Frame;
        }

        public int GetDirection()
        {
            return Direction;
        }

        private int Frame; // Player's hand gesture
        private int Direction; // Player's direction
    }
}
