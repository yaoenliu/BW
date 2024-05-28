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
            //�_�l���
            StartMenu();

            // �}�l�C��
            StartGame();

            //�������
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
                        // �������
                        playerGestureChoice = DetectHandGesture(body);
                    }
                }
            }
        }

        private Enum.Gesture DetectHandGesture(Body body)
        {
            Enum.Gesture gesture = Enum.Gesture.none;
            //  ����⪺���A
            HandState leftHandState = body.HandLeftState;
            HandState rightHandState = body.HandRightState;

            // �ھڤ⪺���A�P�_���
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
            Console.WriteLine("1.�}�l�C��");
            Console.WriteLine("2.�����C��");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            char choice = keyInfo.KeyChar;
            if (choice != '1')
            {
                Environment.Exit(0);
            }
            Console.WriteLine("��J��өһݧ���:");
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

                    // ���ݪ��a���
                    Console.WriteLine($"���a����: {playerScore}  �q������:{computerScore}");
                    Console.WriteLine("�а��X��� (�ŤM, ���Y, ��): ");
                    while (playerGestureChoice == Enum.Gesture.none)
                    {
                        // ���� Kinect ��������
                    }

                    // �q���H���ͦ����
                    Enum.Gesture computerGestureChoice = Enum.Gesture.none;
                    Random random1 = new Random();
                    int computerGestureChoiceIndex = random1.Next(1, 4);
                    switch (computerGestureChoiceIndex)
                    {
                        case 1:
                            computerGestureChoice = Enum.Gesture.rock;
                            Console.WriteLine("�q�����: ���Y");
                            break;
                        case 2:
                            computerGestureChoice = Enum.Gesture.paper;
                            Console.WriteLine("�q�����: ��");
                            break;
                        case 3:
                            computerGestureChoice = Enum.Gesture.scissor;
                            Console.WriteLine("�q�����: �ŤM");
                            break;
                    }

                    // �����աA�M�w�q���ӭt
                    Enum.Player attacker = DetermineFirstWinner(playerGestureChoice, computerGestureChoice);

                    //���⭫�q
                    if (attacker == Player.none)
                        continue;

                    while (playerDirectionChoice == Enum.Direction.none)
                    {
                        // ���� Kinect ������ʧ@
                    }

                    // �q���H���ͦ��ʧ@
                    Enum.Direction computerDirectionChoice = Enum.Direction.none;
                    Random random2 = new Random();
                    int computerDirectionChoiceIndex = random2.Next(1, 5);
                    switch (computerDirectionChoiceIndex)
                    {
                        case 1:
                            computerDirectionChoice = Enum.Direction.up;
                            Console.WriteLine("�q�����: �W");
                            break;
                        case 2:
                            computerDirectionChoice = Enum.Direction.down;
                            Console.WriteLine("�q�����: �U");
                            break;
                        case 3:
                            computerDirectionChoice = Enum.Direction.left;
                            Console.WriteLine("�q�����: ��");
                            break;
                        case 4:
                            computerDirectionChoice = Enum.Direction.right;
                            Console.WriteLine("�q�����: �k");
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

                    //�M�w�ʧ@�ӭt
                    Enum.Player Winner = DetermineSecondWinner(attackerChoice, defenderChoice, attacker);

                    // ���m���a��դΰʧ@
                    playerGestureChoice = Enum.Gesture.none;
                    playerDirectionChoice = Enum.Direction.none;
                }

                // �߰ݬO�_�A���@��
                Console.WriteLine("�n�A���@���ܡH(y/n): ");
                string playAgainInput = Console.ReadLine().Trim().ToLower();
                playAgain = playAgainInput == "y";
            }
        }

        static void EndMenu()
        {
            Console.Clear();
            Console.WriteLine("���¹C���I");
        }
        private Enum.Player DetermineFirstWinner(Enum.Gesture playerChoice, Enum.Gesture computerChoice)
        {
            if (playerChoice == computerChoice)
            {
                Console.WriteLine("�q������");
                return Player.none;
            }

            if ((playerChoice == Enum.Gesture.rock && computerChoice == Enum.Gesture.scissor) ||
                (playerChoice == Enum.Gesture.paper && computerChoice == Enum.Gesture.rock) ||
                (playerChoice == Enum.Gesture.scissor && computerChoice == Enum.Gesture.paper))
            {
                Console.WriteLine("���a�qĹ");
                return Enum.Player.player;
            }
            Console.WriteLine("�q���qĹ");
            return Enum.Player.computer;
        }
        //�P�_����Ĺ�a
        private Enum.Player DetermineSecondWinner(Enum.Direction attackerChoice, Enum.Direction defenderChoice, Enum.Player attacker)
        {
            if (attackerChoice == defenderChoice)
            {
                if (attacker == Player.player)
                {
                    Console.WriteLine("���a���");
                    playerScore += 1;
                }
                else if (attacker == Player.computer)
                {
                    Console.WriteLine("�q�����");
                    computerScore += 1;
                }
                return attacker;
            }
            else
            {
                Console.WriteLine("����");
                return Player.none;
            }
        }
    }
}