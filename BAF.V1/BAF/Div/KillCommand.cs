using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BAF.Div.Div;
using static BAF.Div.ChannelDeclarations;
using static BAF.Shows.WashShows;
using static BAF.Div.SettingAllHeads;

namespace BAF.Div
{
    class KillCommand
    {
        public static void WashKillCommand()
        {
            switch (wash_movement_running)
            {
                case 0:
                    WashKillCommandSetValues();
                    break;
                case 1:
                    MainWindow.WM1.Abort();
                    wash_movement_running = 0;
                    WashKillCommandSetValues();
                    break;
                case 2:
                    MainWindow.WM2.Abort();
                    wash_movement_running = 0;
                    WashKillCommandSetValues();
                    break;
                case 3:
                    MainWindow.WM3.Abort();
                    wash_movement_running = 0;
                    WashKillCommandSetValues();
                    break;
                case 4:
                    MainWindow.WM4.Abort();
                    wash_movement_running = 0;
                    WashKillCommandSetValues();
                    break;
            }
        }

        public static void WashKillCommandSetValues()
        {
            SettingAllLampsEqual(Wash1_Pan, 127);
            SettingAllLampsEqual(Wash1_Tilt, 127);
            SettingAllLampsEqual(Wash1_Shutter, 0);
            SettingAllLampsEqual(Wash1_Rotate, 0);
            SettingAllLampsEqual(Wash1_Split, 0);
            SettingAllLampsEqual(Wash1_Focus, 60);
        }
    }
}
