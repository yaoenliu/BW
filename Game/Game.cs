using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    class Game
    {
        public string status = "";

        Player myPlayer = null;
        Player enemy = null;

        GameManager manager = null;
        SensorWindow sensorWindow = null;
        DispatcherTimer timer = new DispatcherTimer();

        private int RPSWinner = -1;
        private int GameWinner = -1;

        public event EventHandler Start;
        public event EventHandler RPSDone;
        public event EventHandler Tie;
        public event EventHandler Over;

        public Game(GameManager manager, SensorWindow sensorWindow)
        {
            this.manager = manager;
            this.sensorWindow = sensorWindow;
            sensorWindow.enemyImage("running");
            RPSDone += BW;
        }

        public void GameStart()
        {
            Start?.Invoke(this, null);
            myPlayer = new Player();
            enemy = new Player();

            this.RPSWinner = -1;
            this.GameWinner = -1;

            RPS();
        }

        private void RPS()
        {
            this.status = "RPS";
            Debug.WriteLine("RPS");
            sensorWindow.setText("Rock Paper Scissor");
            int enemyHand = randomHandState();
            setEnemyHand(enemyHand);
            Debug.WriteLine("enemyHand : " + enemy.GetHand());
            sensorWindow.setText("Enemy Hand : " + handtoStr(enemy.GetHand()));
            resetTimerEvent();
            timer.Tick += RPSResult;
            timer.Interval = TimeSpan.FromSeconds(manager.WAIT_TIME);
            timer.Start();
        }

        private void RPSResult(object sender, EventArgs e)
        {
            timer.Stop();
            sensorWindow.enemyImage(handtoStr(enemy.GetHand()));
            int myplayerHand = manager.getCurHandState();
            if (myplayerHand < 0)
            {
                RPS();
                return;
            }

            setPlayerHand(myplayerHand);
            Debug.WriteLine("myplayerHand : " + myplayerHand);

            RPSWinner = RockPaperScissor(myPlayer, enemy);
            Debug.WriteLine("RPSWinner : " + RPSWinner);
            sensorWindow.setText("RPSWinner : " + RPSWinner);

            RPSDone?.Invoke(this, null);
        }

        private void BW(object s = null, EventArgs e = null)
        {
            this.status = "BW";
            Debug.WriteLine("BW");

            int enemyDir = randomDirState();
            setEnemyDir(enemyDir);
            Debug.WriteLine("enemyDir : " + enemy.GetDirection());

            timer.Interval = TimeSpan.FromSeconds(manager.WAIT_TIME);
            resetTimerEvent();
            timer.Tick += BWResult;
            timer.Start();
        }

        private void BWResult(object sender, EventArgs e)
        {
            timer.Stop();

            int myplayerDir = manager.getCurDirState();
            if (myplayerDir < 0)
            {
                BW();
                return;
            }

            setPlayerDir(myplayerDir);
            Debug.WriteLine("myplayerDir : " + myplayerDir);

            int same = blackWhite(myPlayer, enemy);
            if (same == 1)
            {
                GameWinner = RPSWinner;
                Over?.Invoke(this, null);
                Debug.WriteLine("GameWinner : " + GameWinner);
            }
            else
            {
                GameWinner = -1;
                Tie?.Invoke(this, null);
                Debug.WriteLine("Tie");
            }
            return;
        }

        private void resetTimerEvent()
        {
            timer.Tick -= RPSResult;
            timer.Tick -= BWResult;
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
            enemy.SetFrame(handState);
        }
        public int getEnemyHand()
        {
            return enemy.GetHand();
        }
        public void setEnemyDir(int dirState)
        {
            enemy.SetDirection(dirState);
        }
        public int getEnemyDir()
        {
            return enemy.GetDirection();
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
        public int GetRPSWinner()
        {
            return RPSWinner;
        }

        public int GetGameWinner()
        {
            return GameWinner;
        }

        public string GetStatus()
        {
            return status;
        }

        private int RockPaperScissor(Player myPlayer, Player Enemy)
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
                    else if (enemyHand == 3)
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
        private int blackWhite(Player myPlayer, Player Enemy)
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

        public string handtoStr(int hand)
        {
            string str = "";
            switch (hand)
            {
                case 1:
                    str = "Rock";
                    break;
                case 2:
                    str = "Paper";
                    break;
                case 3:
                    str = "Scissors";
                    break;
            }
            return str;
        }

        public string dirtoStr(int dir)
        {
            string str = "";
            switch (dir)
            {
                case 0:
                    str = "Up";
                    break;
                case 1:
                    str = "Left";
                    break;
                case 2:
                    str = "Down";
                    break;
                case 3:
                    str = "Right";
                    break;
            }
            return str;
        }
    }

}

