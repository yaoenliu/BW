using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Kinect;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;
using static Enum;

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
        private Enum.Gesture playerGestureChoice = Enum.Gesture.none;
        static int winScore = 1, playerScore = 0, computerScore = 0;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //起始選單
            StartMenu();

            // 開始遊戲
            StartGame();

            //結束選單
            EndMenu();

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
                        playerGestureChoice = DetectHandGesture(body);
                    }
                }
            }
        }

        private Enum.Gesture DetectHandGesture(Body body)
        {
            Enum.Gesture gesture = Enum.Gesture.none;
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
        static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine("1.開始遊戲");
            Console.WriteLine("2.結束遊戲");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            char choice = keyInfo.KeyChar;
            if (choice != '1')
            {
                Environment.Exit(0);
            }
            Console.WriteLine("輸入獲勝所需局數:");
            winScore = Console.Read();
        }

        private void StartGame()
        {
            bool playAgain = true;
            while (playAgain)
            {
                while (Math.Max(playerScore, computerScore) < winScore)
                {
                    Console.Clear();

                    // 等待玩家手勢
                    Console.WriteLine($"玩家分數: {playerScore}  電腦分數:{computerScore}");
                    Console.WriteLine("請做出手勢 (剪刀, 石頭, 布): ");
                    while (playerGestureChoice == Enum.Gesture.none)
                    {
                        // 等待 Kinect 偵測到手勢
                    }

                    // 電腦隨機生成手勢
                    Enum.Gesture computerGestureChoice = Enum.Gesture.none;
                    Random random1 = new Random();
                    int computerGestureChoiceIndex = random1.Next(1, 4);
                    switch (computerGestureChoiceIndex)
                    {
                        case 1:
                            computerGestureChoice = Enum.Gesture.rock;
                            Console.WriteLine("電腦選擇: 石頭");
                            break;
                        case 2:
                            computerGestureChoice = Enum.Gesture.paper;
                            Console.WriteLine("電腦選擇: 布");
                            break;
                        case 3:
                            computerGestureChoice = Enum.Gesture.scissor;
                            Console.WriteLine("電腦選擇: 剪刀");
                            break;
                    }

                    // 比較手勢，決定猜拳勝負
                    Enum.Player attacker = DetermineFirstWinner(playerGestureChoice, computerGestureChoice);

                    //平手重猜
                    if (attacker == Player.none)
                        continue;

                    while (playerDirectionChoice == Enum.Direction.none)
                    {
                        // 等待 Kinect 偵測到動作
                    }

                    // 電腦隨機生成動作
                    Enum.Direction computerDirectionChoice = Enum.Direction.none;
                    Random random2 = new Random();
                    int computerDirectionChoiceIndex = random2.Next(1, 5);
                    switch (computerDirectionChoiceIndex)
                    {
                        case 1:
                            computerDirectionChoice = Enum.Direction.up;
                            Console.WriteLine("電腦選擇: 上");
                            break;
                        case 2:
                            computerDirectionChoice = Enum.Direction.down;
                            Console.WriteLine("電腦選擇: 下");
                            break;
                        case 3:
                            computerDirectionChoice = Enum.Direction.left;
                            Console.WriteLine("電腦選擇: 左");
                            break;
                        case 4:
                            computerDirectionChoice = Enum.Direction.right;
                            Console.WriteLine("電腦選擇: 右");
                            break;
                    }

                    Direction attackerChoice = Direction.none, defenderChoice = Direction.none;

                    if (attacker == Player.player)
                    {
                        attackerChoice = playerDirectionChoice;
                        defenderChoice = computerDirectionChoice;
                    }
                    else
                    {
                        attackerChoice = computerDirectionChoice;
                        defenderChoice = playerDirectionChoice;
                    }

                    //決定動作勝負
                    Enum.Player Winner = DetermineSecondWinner(attackerChoice, defenderChoice, attacker);

                    // 重置玩家手勢及動作
                    playerGestureChoice = Enum.Gesture.none;
                    playerDirectionChoice = Enum.Direction.none;
                }

                // 詢問是否再玩一局
                Console.WriteLine("要再玩一局嗎？(y/n): ");
                string playAgainInput = Console.ReadLine().Trim().ToLower();
                playAgain = playAgainInput == "y";
            }
        }

        static void EndMenu()
        {
            Console.Clear();
            Console.WriteLine("謝謝遊玩！");
        }
        private Enum.Player DetermineFirstWinner(Enum.Gesture playerChoice, Enum.Gesture computerChoice)
        {
            if (playerChoice == computerChoice)
            {
                Console.WriteLine("猜拳平手");
                return Player.none;
            }

            if ((playerChoice == Enum.Gesture.rock && computerChoice == Enum.Gesture.scissor) ||
                (playerChoice == Enum.Gesture.paper && computerChoice == Enum.Gesture.rock) ||
                (playerChoice == Enum.Gesture.scissor && computerChoice == Enum.Gesture.paper))
            {
                Console.WriteLine("玩家猜贏");
                return Enum.Player.player;
            }
            Console.WriteLine("電腦猜贏");
            return Enum.Player.computer;
        }
        //判斷攻擊贏家
        private Enum.Player DetermineSecondWinner(Enum.Direction attackerChoice, Enum.Direction defenderChoice, Enum.Player attacker)
        {
            if (attackerChoice == defenderChoice)
            {
                if (attacker == Player.player)
                {
                    Console.WriteLine("玩家獲勝");
                    playerScore += 1;
                }
                else if (attacker == Player.computer)
                {
                    Console.WriteLine("電腦獲勝");
                    computerScore += 1;
                }
                return attacker;
            }
            else
            {
                Console.WriteLine("平手");
                return Player.none;
            }
        }
    }
}