using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Kinect;
using System.ComponentModel;
using System.Threading;

namespace BlackWhiteCutGame
{
    public partial class MainWindow : Window
    {
        private KinectSensor kinectSensor = null;
        private BodyFrameReader bodyFrameReader = null;
        private Body[] bodies = null;
        private string playerChoice = string.Empty;

        public MainWindow()
        {
            // ��l�� Kinect
            this.kinectSensor = KinectSensor.GetDefault();
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();
            this.kinectSensor.Open();

            this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // �}�l�C��
            StartGame();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
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
                //  �Ĥ@������⪺���A
                HandState leftHandState1 = body.HandLeftState;
                HandState rightHandState1 = body.HandRightState;
                System.Threading.Thread.Sleep(1000);
                //  �ĤG������⪺���A
                HandState leftHandState2 = body.HandLeftState;
                HandState rightHandState2 = body.HandRightState;
                if(leftHandState1 != null && rightHandState1 != null && leftHandState2 != null && rightHandState2 != null) {
                    if (leftHandState1 == leftHandState2 && rightHandState1 == rightHandState2) {
                        break;
            }

            // �ھڤ⪺���A�P�_���
            if (leftHandState1 == HandState.Closed && rightHandState1 == HandState.Closed)
            {
                gesture = "���Y";
            }
            else if (leftHandState1 == HandState.Open && rightHandState1 == HandState.Open)
            {
                gesture = "��";
            }
            else if (leftHandState1 == HandState.Lasso || rightHandState1 == HandState.Lasso)
            {
                gesture = "�ŤM";
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
