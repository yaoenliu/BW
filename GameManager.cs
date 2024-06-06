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
        SensorWindow sensorWindow = null;
        Game game = null;
        public int WAIT_TIME = 3;

        public GameManager()
        {
            
            sensorWindow = new SensorWindow();
            sensorWindow.ValidInCamera += GameStart;
            sensorWindow.Show();
            sensorWindow.Hide();
            sensorWindow.HandChanged += onHandChanged;
            sensorWindow.DirChanged += onDirChanged;
            this.game = new Game(this);
            game.Tie += GameStart;
        }

        void GameStart(object sender, EventArgs e)
        {
            if (!sensorWindow.GetSensorWorking())
            {
                Debug.WriteLine("Sensor not working");
                return;
            }
            
            game.GameStart();
        }

        void onHandChanged(object sender,  EventArgs e)
        {
            //Debug.WriteLine("Dir : " + sensorWindow.GetCurDirState() + " Hand: " + sensorWindow.GetCurHandState());
        }

        void onDirChanged(object sender, EventArgs e)
        {
            //Debug.WriteLine("Dir : " + sensorWindow.GetCurDirState() + " Hand: " + sensorWindow.GetCurHandState());
        }
        
        public int getCurHandState()
        {
            int hand = sensorWindow.GetCurHandState();
            return hand;
        }

        public int getCurDirState()
        {
            int dir = sensorWindow.GetCurDirState();
            return dir;
        }
    }
}
