using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Kinect;
using System.ComponentModel;
using System.Threading;

namespace BlackWhiteCutGame
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private KinectSensor kinectSensor = null;
        private BodyFrameReader bodyFrameReader = null;
        private Body[] bodies = null;
        private string playerChoice = string.Empty;


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 開始遊戲
            StartGame();
        }

        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                foreach (Body body in this.bodies)
                {
                    if (body.IsTracked)
                    {
                        // 偵測手勢
                        playerChoice = DetectHandGesture(body);
                    }
                }
            }
        }

        private string DetectHandGesture(Body body)
        {
            string gesture = string.Empty;
            while (true)
            {
                //  獲取手的狀態
                HandState leftHandState = body.HandLeftState;
                HandState rightHandState = body.HandRightState;

                // 根據手的狀態判斷手勢
                if (leftHandState == HandState.Closed || rightHandState == HandState.Closed)
                {
                    gesture = "石頭";
                }
                else if (leftHandState == HandState.Open || rightHandState == HandState.Open)
                {
                    gesture = "布";
                }
                else if (leftHandState == HandState.Lasso || rightHandState == HandState.Lasso)
                {
                    gesture = "剪刀";
                }

            }
            return gesture;
        }


        private void StartGame()
        {
            bool playAgain = true;

            while (playAgain)
            {
                // 等待玩家手勢
                Console.WriteLine("請做出手勢 (剪刀, 石頭, 布): ");
                while (string.IsNullOrEmpty(playerChoice))
                {
                    // 等待 Kinect 偵測到手勢
                }

                // 電腦隨機生成手勢
                string[] choices = { "剪刀", "石頭", "布" };
                Random random = new Random();
                int computerChoiceIndex = random.Next(choices.Length);
                string computerChoice = choices[computerChoiceIndex];

                // 顯示電腦選擇
                Console.WriteLine($"電腦選擇: {computerChoice}");

                // 比較手勢，決定勝負
                string result = DetermineWinner(playerChoice, computerChoice);
                Console.WriteLine(result);

                // 詢問是否再玩一局
                Console.WriteLine("要再玩一局嗎？(y/n): ");
                string playAgainInput = Console.ReadLine().Trim().ToLower();
                playAgain = playAgainInput == "y";

                // 重置玩家手勢
                playerChoice = string.Empty;
            }

            Console.WriteLine("謝謝遊玩！");
        }

        private string DetermineWinner(string playerChoice, string computerChoice)
        {
            if (playerChoice == computerChoice)
            {
                return "平手!";
            }

            if ((playerChoice == "剪刀" && computerChoice == "布") ||
                (playerChoice == "石頭" && computerChoice == "剪刀") ||
                (playerChoice == "布" && computerChoice == "石頭"))
            {
                return "你贏了!";
            }

            return "你輸了!";
        }
    }
}