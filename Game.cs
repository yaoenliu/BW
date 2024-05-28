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
            // �}�l�C��
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
                        // �������
                        playerChoice = DetectHandGesture(body);
                    }
                }
            }
        }

        private Enum.Gesture DetectHandGesture(Body body)
        {
            Enum.Gesture gesture = Enum.Gesture.None;
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


        private void StartGame()
        {
            bool playAgain = true;

            while (playAgain)
            {
                // ���ݪ��a���
                Console.WriteLine("�а��X��� (�ŤM, ���Y, ��): ");
                while (playerChoice == Enum.Gesture.None)
                {
                    // ���� Kinect ��������
                }

                // �q���H���ͦ����
                Enum.Gesture computerChoice = Enum.Gesture.None;
                Random random = new Random();
                int computerChoiceIndex = random.Next(1, 4);
                switch (computerChoiceIndex)
                {
                    case 1:
                        computerChoice = Enum.Gesture.rock;
                        Console.WriteLine("�q�����: ���Y");
                        break;
                    case 2:
                        computerChoice = Enum.Gesture.paper;
                        Console.WriteLine("�q�����: ��");
                        break;
                    case 3:
                        computerChoice = Enum.Gesture.scissor;
                        Console.WriteLine("�q�����: �ŤM");
                        break;
                }


                // �����աA�M�w�ӭt
                string result = DetermineWinner(playerChoice, computerChoice);
                Console.WriteLine(result);

                // �߰ݬO�_�A���@��
                Console.WriteLine("�n�A���@���ܡH(y/n): ");
                string playAgainInput = Console.ReadLine().Trim().ToLower();
                playAgain = playAgainInput == "y";

                // ���m���a���
                playerChoice = Enum.Gesture.None;
            }

            Console.WriteLine("���¹C���I");
        }

        private string DetermineWinner(Enum.Gesture playerChoice, Enum.Gesture computerChoice)
        {
            if (playerChoice == computerChoice)
            {
                return "����!";
            }

            if ((playerChoice == Enum.Gesture.rock && computerChoice == Enum.Gesture.scissor) ||
                (playerChoice == Enum.Gesture.paper && computerChoice == Enum.Gesture.rock) ||
                (playerChoice == Enum.Gesture.scissor && computerChoice == Enum.Gesture.paper))
            {
                return "�AĹ�F!";
            }

            return "�A��F!";
        }
    }
}