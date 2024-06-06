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
            game = new Game(this, sensorWindow);
            bindings();
            sensorWindow.Show();
        }

        void bindings()
        {
            sensorWindow.ValidInCamera += GameStart;
            sensorWindow.HandChanged += onHandChanged;
            sensorWindow.DirChanged += onDirChanged;
            game.Tie += GameStart;
        }

        void GameStart(object sender, EventArgs e)
        {
            game.GameStart();
        }

        void onHandChanged(object sender,  EventArgs e)
        {
            if (game.status == "RPS")
            {
                sensorWindow.playerImage(game.handtoStr(sensorWindow.CurHandState));
            }
        }

        void onDirChanged(object sender, EventArgs e)
        {
           if (game.status == "BW")
            {
                sensorWindow.playerImage(game.dirtoStr(sensorWindow.CurDirState));
            }
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
