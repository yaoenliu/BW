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
            Frame = "";
            Direction = "";
        }

        public void SetFrame(string hand)
        {
            Frame = hand;
        }

        public void SetDirection(string hand)
        {
            Direction = hand;
        }

        public string GetHand()
        {
            return Frame;
        }

        public string GetDirection()
        {
            return Direction;
        }

        private string Frame; // Player's hand gesture
        private string Direction; // Player's direction
    }
}
