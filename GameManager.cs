﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            sensorWindow.Hide();
            getCurDir();
            getCurHand();
            getSensorState();
        }
       
        private void getSensorState()
        {
            this.sensorState = sensorWindow.GetSensorWorking();
        }

        private void getCurDir()
        {
            this.curDir = sensorWindow.GetCurDirState();
        }

        private void getCurHand()
        {
            this.curHand = sensorWindow.GetCurHandState();
        }
    }
}
