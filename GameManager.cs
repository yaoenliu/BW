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
            sensorWindow.Hide();
            sensorWindow.HandChanged += onHandChanged;
            sensorWindow.DirChanged += onDirChanged;
        }

        void onHandChanged(object sender,  EventArgs e)
        {
            curHand = sensorWindow.GetCurHandState();
            Debug.WriteLine("Dir : " + curDir + " Hand: " + curHand);
        }

        void onDirChanged(object sender, EventArgs e)
        {
            curDir = sensorWindow.GetCurDirState();
            Debug.WriteLine("Dir : " + curDir + " Hand: " + curHand);
        }

    }
}
