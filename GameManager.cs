using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;


namespace Microsoft.Samples.Kinect.BodyBasics
{
    class GameManager
    {
        int curDir = -1;
        int curHand = -1;
        bool sensorState = false;

        SensorWindow sensorWindow = new SensorWindow();

        public GameManager()
        {
            sensorWindow.Show();
            sensorWindow.HandChanged += onHandChanged;
            sensorWindow.DirChanged += onDirChanged;
            countdown();
            //setImage("stone");
        }

        void onHandChanged(object sender, EventArgs e)
        {
            curHand = sensorWindow.GetCurHandState();
            Debug.WriteLine("Dir : " + curDir + " Hand: " + curHand);
        }

        void onDirChanged(object sender, EventArgs e)
        {
            curDir = sensorWindow.GetCurDirState();
            Debug.WriteLine("Dir : " + curDir + " Hand: " + curHand);
        }

        void Countdown(int seconds)
        {
            sensorWindow.Dispatcher.Invoke(() =>
            {
                if(seconds == 0)
                {
                    sensorWindow.countDown.Visibility = Visibility.Hidden;
                    return;
                }
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, "../../../Images/" + seconds + ".png");
                bitmapImage.UriSource = new Uri(path);
                bitmapImage.EndInit();
                sensorWindow.countDown.Source = bitmapImage;
            });

        }

        void setImage(string content)
        {
            sensorWindow.Dispatcher.Invoke(() =>
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, "../../../Images/" + content + ".png");
                bitmapImage.UriSource = new Uri(path);
                bitmapImage.EndInit();
                sensorWindow.gameShow.Source = bitmapImage;
            });
        }

        async void countdown()
        {
            sensorWindow.Dispatcher.Invoke(() =>
            {
                sensorWindow.countDown.Visibility = Visibility.Visible;
            });
            Countdown(3);
            await Task.Delay(1000);
            Countdown(2);
            await Task.Delay(1000);
            Countdown(1);
            await Task.Delay(1000);
            Countdown(0);
        }
    }
}
