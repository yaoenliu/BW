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
        private Enum.Gesture playerChoice = Enum.Gesture.None;


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

        private Enum.Gesture DetectHandGesture(Body body)
        {
            Enum.Gesture gesture = Enum.Gesture.None;
            //  獲取手的狀態
            HandState leftHandState = body.HandLeftState;
            HandState rightHandState = body.HandRightState;

            // 根據手的狀態判斷手勢
            if (leftHandState == HandState.Closed || rightHandState == HandState.Closed)
            {
                gesture = Enum.Gesture.rock;
            }
            else if (leftHandState == HandState.Open || rightHandState == HandState.Open)
            {
                gesture = Enum.Gesture.paper;
            }
            else if (leftHandState == HandState.Lasso || rightHandState == HandState.Lasso)
            {
                gesture = Enum.Gesture.scissor;
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
                while (playerChoice == Enum.Gesture.None)
                {
                    // 等待 Kinect 偵測到手勢
                }

                // 電腦隨機生成手勢
                Enum.Gesture computerChoice = Enum.Gesture.None;
                Random random = new Random();
                int computerChoiceIndex = random.Next(1, 4);
                switch (computerChoiceIndex)
                {
                    case 1:
                        computerChoice = Enum.Gesture.rock;
                        Console.WriteLine("電腦選擇: 石頭");
                        break;
                    case 2:
                        computerChoice = Enum.Gesture.paper;
                        Console.WriteLine("電腦選擇: 布");
                        break;
                    case 3:
                        computerChoice = Enum.Gesture.scissor;
                        Console.WriteLine("電腦選擇: 剪刀");
                        break;
                }


                // 比較手勢，決定勝負
                string result = DetermineWinner(playerChoice, computerChoice);
                Console.WriteLine(result);

                // 詢問是否再玩一局
                Console.WriteLine("要再玩一局嗎？(y/n): ");
                string playAgainInput = Console.ReadLine().Trim().ToLower();
                playAgain = playAgainInput == "y";

                // 重置玩家手勢
                playerChoice = Enum.Gesture.None;
            }

            Console.WriteLine("謝謝遊玩！");
        }

        private string DetermineWinner(Enum.Gesture playerChoice, Enum.Gesture computerChoice)
        {
            if (playerChoice == computerChoice)
            {
                return "平手!";
            }

            if ((playerChoice == Enum.Gesture.rock && computerChoice == Enum.Gesture.scissor) ||
                (playerChoice == Enum.Gesture.paper && computerChoice == Enum.Gesture.rock) ||
                (playerChoice == Enum.Gesture.scissor && computerChoice == Enum.Gesture.paper))
            {
                return "你贏了!";
            }

            return "你輸了!";
        }
    }
}