using System;
using System.IO.Ports;
using System.Threading;
using System.Windows;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    class Game
    {
        string status = "";
        Player myPlayer;
        Player Enemy;
        public Game() {
            status = "OK";
            myPlayer = new Player();
            Enemy = new Player();
        }
        public void setPlayerHand(int handState)
        {
            myPlayer.SetFrame(handState);
        }
        public int getPlayerHand()
        {
            return myPlayer.GetHand();
        }
        public void setPlayerDir(int dirState)
        {
            myPlayer.SetDirection(dirState);
        }
        public int getPlayerDir()
        {
            return myPlayer.GetDirection();
        }
        public void setEnemyHand(int handState)
        {
            Enemy.SetFrame(handState);
        }
        public int getEnemyHand()
        {
            return Enemy.GetHand();
        }
        public void setEnemyDir(int dirState)
        {
            Enemy.SetDirection(dirState);
        }
        public int getEnemyDir()
        {
            return Enemy.GetDirection();
        }
        public int randomHandState()
        {
            int hand = new Random().Next(1, 3);
            return hand;
        }
        public int randomDirState()
        {
            int Dir = new Random().Next(0, 3);
            return Dir;
        }
        public int RockPaperScissor(Player myPlayer,Player Enemy)
        {
            int winner = -1;    //0 is draw,1 is i win,2 is enemy win
            int myHand = myPlayer.GetHand();
            int enemyHand = Enemy.GetHand();
            switch (myHand)
            {
                case 1:     //myHand is Rock
                    if (enemyHand == 1)
                        winner = 0;
                    else if (enemyHand == 2)
                        winner = 2;
                    else if (enemyHand == 3)
                        winner = 1;
                    break;
                case 2: //myHand is paper
                    if (enemyHand == 1)
                        winner = 1;
                    else if (enemyHand == 2)
                        winner = 0;
                    else if(enemyHand == 3)
                        winner = 2;
                    break;
                case 3: //myHand is scissor
                    if (enemyHand == 1)
                        winner = 2;
                    else if (enemyHand == 2)
                        winner = 1;
                    else if (enemyHand == 3)
                        winner = 0;
                    break;
            }
            return winner;
        }
        public int blackWhite(Player myPlayer,Player Enemy)
        {
            int isSame = -1;    //if 1 then direction is same else direction is different
            int myDir = myPlayer.GetDirection();
            int enemyDir = Enemy.GetDirection();
            if (myDir == enemyDir)
                isSame = 1;
            else
                isSame = 0;
            return isSame; 
        }
    }

}

