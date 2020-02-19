using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using static BAF.Div.Div;
using BAF.Shows;
using static BAF.Shows.WashShows;
using static BAF.Div.KillCommand;
using static BAF.Div.ChannelDeclarations;
using static BAF.Div.SettingAllHeads;

namespace BAF
{
    public partial class MainWindow : Window
    {
        public byte whiteLed_chosen_color = 0;

        public static Thread WM1;
        public static Thread WM2;
        public static Thread WM3;
        public static Thread WM4;
        public static Thread WR;

        byte Wash_Gobo_Circle = 0;
        byte Wash_Gobo_Triangle = 36;
        byte Wash_Gobo_CircleRing = 82;
        byte Wash_Gobo_Star = 101;
        byte Wash_Gobo_Flower = 181;
        byte Wash_Gobo_Wheel = 142;
        byte Wash_Gobo_Palette = 54;

        public static List<BaseClass.Objects.LightDefinitions.CosmoLight> myLights;
        public static int ColorDuration;
        public static int MovementDuration;
        public static int chosen_color;
        public static Thread WCC;

        public static bool RotateOn = false;

        public MainWindow()
        {
            InitializeComponent();
            OpenDMX.start();
            EnttecStatus();
            WashKillCommand();
            MovementKillCommandBtnSetter();

            Thread t = new Thread(DataThread);
            t.Start();
        }

        public static void DataThread()
        {
            while (true)
            {
                OpenDMX.writeData();
                Thread.Sleep(50);
            }
        }

        private void EnttecStatus()
        {
            try
            {
                if (OpenDMX.status == FT_STATUS.FT_DEVICE_NOT_FOUND)
                {
                    StatusBox.Text = "No Enttec USB Device Found";
                }
                else if (OpenDMX.status == FT_STATUS.FT_OK)
                {
                    StatusBox.Text = "Found DMX on USB";
                }
                else
                    StatusBox.Text = "Error Opening Device";
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                StatusBox.Text = "Error Connecting to Enttec USB Device";
            }
        }

        private void Wash_Color_Red_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            SettingAllLampsEqual(Wash1_Shutter, 255);
            SettingAllLampsEqual(Wash1_Color, 30);
        }

        private void Wash_Color_Change_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            SettingAllLampsEqual(Wash1_Shutter, 255);
            SettingAllLampsEqual(Wash1_Color, 203);
        }

        private void Wash_Color_Green_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            SettingAllLampsEqual(Wash1_Shutter, 255);
            SettingAllLampsEqual(Wash1_Color, 97);
        }

        private void Wash_Color_Blue_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            SettingAllLampsEqual(Wash1_Shutter, 255);
            SettingAllLampsEqual(Wash1_Color, 151);
        }

        private void Wash_Color_White_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            SettingAllLampsEqual(Wash1_Shutter, 255);
            SettingAllLampsEqual(Wash1_Color, 0);
        }

        private void Wash_Movement1_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            MovementKillCommandBtnSetter();
            Wash_Movement1.Background = Brushes.LightGreen;
            WM1 = new Thread(WashMovement1_thread);
            WM1.Start();
        }

        private void Wash_Movement2_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            MovementKillCommandBtnSetter();
            Wash_Movement2.Background = Brushes.LightGreen;
            WM2 = new Thread(WashMovement2_thread);
            WM2.Start();
        }

        private void Wash_Movement3_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            WM3 = new Thread(WashMovement3_thread);
            WM3.Start();
        }

        private void Wash_Movement4_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            WM4 = new Thread(WashMovement4_thread);
            WM4.Start();
        }

        private void Wash_Off_Click(object sender, RoutedEventArgs e)
        {
            WashKillCommand();
            MovementKillCommandBtnSetter();
            Gobo_Rotate_Btn.Background = Brushes.LightGray;
            GoboKillCommandBtnSetter();
        }

        private void Wash_Reset_Btn_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            WR = new Thread(WashReset_thread);
            WR.Start();
        }

        private void Wash_Color_Yellow_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            SettingAllLampsEqual(Wash1_Shutter, 255);
            SettingAllLampsEqual(Wash1_Color, 82);
        }

        private void Wash_Gobo_Circle_Btn_Click(object sender, RoutedEventArgs e)
        {
            SettingAllLampsEqual(Wash1_Gobo, Wash_Gobo_Circle);
            GoboKillCommandBtnSetter();
            Wash_Gobo_Circle_Btn.Background = Brushes.LightGreen;
        }

        private void Wash_Gobo_Triangle_Btn_Click(object sender, RoutedEventArgs e)
        {
            SettingAllLampsEqual(Wash1_Gobo, Wash_Gobo_Triangle);
            GoboKillCommandBtnSetter();
            Wash_Gobo_Triangle_Btn.Background = Brushes.LightGreen;
        }

        private void Wash_Gobo_CircleRing_Btn_Click(object sender, RoutedEventArgs e)
        {
            SettingAllLampsEqual(Wash1_Gobo, Wash_Gobo_CircleRing);
            GoboKillCommandBtnSetter();
            Wash_Gobo_CircleRing_Btn.Background = Brushes.LightGreen;
        }

        private void Wash_Gobo_Star_Btn_Click(object sender, RoutedEventArgs e)
        {
            SettingAllLampsEqual(Wash1_Gobo, Wash_Gobo_Star);
            GoboKillCommandBtnSetter();
            Wash_Gobo_Star_Btn.Background = Brushes.LightGreen;
        }

        private void Wash_Gobo_Flower_Btn_Click(object sender, RoutedEventArgs e)
        {
            SettingAllLampsEqual(Wash1_Gobo, Wash_Gobo_Flower);
            GoboKillCommandBtnSetter();
            Wash_Gobo_Flower_Btn.Background = Brushes.LightGreen;
        }

        private void Wash_Gobo_Wheel_Btn_Click(object sender, RoutedEventArgs e)
        {
            SettingAllLampsEqual(Wash1_Gobo, Wash_Gobo_Wheel);
            GoboKillCommandBtnSetter();
            Wash_Gobo_Wheel_Btn.Background = Brushes.LightGreen;
        }

        private void Wash_Gobo_Palette_Btn_Click(object sender, RoutedEventArgs e)
        {
            SettingAllLampsEqual(Wash1_Gobo, Wash_Gobo_Palette);
            GoboKillCommandBtnSetter();
            Wash_Gobo_Palette_Btn.Background = Brushes.LightGreen;
        }

        private void Gobo_Rotate_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (RotateOn)
            {
                SettingAllLampsEqual(Wash1_Rotate, 0);
                RotateOn = false;
                Gobo_Rotate_Btn.Background = Brushes.LightGray;
            }
            else
            {
                SettingAllLampsEqual(Wash1_Rotate, 110);
                RotateOn = true;
                Gobo_Rotate_Btn.Background = Brushes.LightGreen;
            }
        }

        private void Grav_Btn_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            WashKillCommand();
            MovementKillCommandBtnSetter();
            Grav_Btn.Background = Brushes.LightGreen;
            SettingAllLampsEqual(Wash1_Shutter, 255);

            OpenDMX.setDmxValue(Wash5_Tilt, 72);
            OpenDMX.setDmxValue(Wash5_Pan, 124);

            OpenDMX.setDmxValue(Wash6_Tilt, 70);
            OpenDMX.setDmxValue(Wash6_Pan, 130);

            OpenDMX.setDmxValue(Wash3_Tilt, 180);
            OpenDMX.setDmxValue(Wash3_Pan, 123);

            OpenDMX.setDmxValue(Wash4_Tilt, 180);
            OpenDMX.setDmxValue(Wash4_Pan, 123);

            OpenDMX.setDmxValue(Wash2_Tilt, 180);
            OpenDMX.setDmxValue(Wash2_Pan, 110);

            OpenDMX.setDmxValue(Wash1_Tilt, 180);
            OpenDMX.setDmxValue(Wash1_Pan, 110);
        }

        private void Præmie_Btn_Click(object sender, RoutedEventArgs e)
        {
            EnttecStatus();
            WashKillCommand();
            MovementKillCommandBtnSetter();
            Præmie_Btn.Background = Brushes.LightGreen;
            SettingAllLampsEqual(Wash1_Shutter, 255);

            OpenDMX.setDmxValue(Wash1_Tilt, 40);
            OpenDMX.setDmxValue(Wash1_Pan, 175);

            OpenDMX.setDmxValue(Wash2_Tilt, 43);
            OpenDMX.setDmxValue(Wash2_Pan, 175);

            OpenDMX.setDmxValue(Wash3_Tilt, 42);
            OpenDMX.setDmxValue(Wash3_Pan, 180);

            OpenDMX.setDmxValue(Wash4_Tilt, 40);
            OpenDMX.setDmxValue(Wash4_Pan, 180);

            OpenDMX.setDmxValue(Wash5_Tilt, 60);
            OpenDMX.setDmxValue(Wash5_Pan, 100);

            OpenDMX.setDmxValue(Wash6_Tilt, 60);
            OpenDMX.setDmxValue(Wash6_Pan, 104);
        }

        public void MovementKillCommandBtnSetter()
        {
            Wash_Movement1.Background = Brushes.LightGray;
            Wash_Movement2.Background = Brushes.LightGray;
            Wash_Movement3.Background = Brushes.LightGray;
            Wash_Movement4.Background = Brushes.LightGray;
            Præmie_Btn.Background = Brushes.LightGray;
            Grav_Btn.Background = Brushes.LightGray;
        }

        public void GoboKillCommandBtnSetter()
        {
            Wash_Gobo_Circle_Btn.Background = Brushes.LightGray;
            Wash_Gobo_Triangle_Btn.Background = Brushes.LightGray;
            Wash_Gobo_CircleRing_Btn.Background = Brushes.LightGray;
            Wash_Gobo_Star_Btn.Background = Brushes.LightGray;
            Wash_Gobo_Flower_Btn.Background = Brushes.LightGray;
            Wash_Gobo_Wheel_Btn.Background = Brushes.LightGray;
            Wash_Gobo_Palette_Btn.Background = Brushes.LightGray;
        }
    }
}