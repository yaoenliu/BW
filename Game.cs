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

        private string DetectHandGesture(Body body)
        {
            string gesture = string.Empty;
            while (true)
            {
                //  ����⪺���A
                HandState leftHandState = body.HandLeftState;
                HandState rightHandState = body.HandRightState;

                // �ھڤ⪺���A�P�_���
                if (leftHandState == HandState.Closed || rightHandState == HandState.Closed)
                {
                    gesture = "���Y";
                }
                else if (leftHandState == HandState.Open || rightHandState == HandState.Open)
                {
                    gesture = "��";
                }
                else if (leftHandState == HandState.Lasso || rightHandState == HandState.Lasso)
                {
                    gesture = "�ŤM";
                }

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
                while (string.IsNullOrEmpty(playerChoice))
                {
                    // ���� Kinect ��������
                }

                // �q���H���ͦ����
                string[] choices = { "�ŤM", "���Y", "��" };
                Random random = new Random();
                int computerChoiceIndex = random.Next(choices.Length);
                string computerChoice = choices[computerChoiceIndex];

                // ��ܹq�����
                Console.WriteLine($"�q�����: {computerChoice}");

                // �����աA�M�w�ӭt
                string result = DetermineWinner(playerChoice, computerChoice);
                Console.WriteLine(result);

                // �߰ݬO�_�A���@��
                Console.WriteLine("�n�A���@���ܡH(y/n): ");
                string playAgainInput = Console.ReadLine().Trim().ToLower();
                playAgain = playAgainInput == "y";

                // ���m���a���
                playerChoice = string.Empty;
            }

            Console.WriteLine("���¹C���I");
        }

        private string DetermineWinner(string playerChoice, string computerChoice)
        {
            if (playerChoice == computerChoice)
            {
                return "����!";
            }

            if ((playerChoice == "�ŤM" && computerChoice == "��") ||
                (playerChoice == "���Y" && computerChoice == "�ŤM") ||
                (playerChoice == "��" && computerChoice == "���Y"))
            {
                return "�AĹ�F!";
            }

            return "�A��F!";
        }
    }
}