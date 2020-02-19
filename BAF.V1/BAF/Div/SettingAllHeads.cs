using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BAF.Div.ChannelDeclarations;
using static BAF.Div.Div;

namespace BAF.Div
{
    class SettingAllHeads
    {
        public static int Lamp1StartingValue;
        public static void SettingAllLampsEqual(int Lamp1Channel, byte Value)
        {
            Lamp1StartingValue = Lamp1Channel;
            for (Lamp1Channel = Lamp1Channel; Lamp1Channel <= Lamp1StartingValue + 50; Lamp1Channel = Lamp1Channel + Offset)
            {
                OpenDMX.setDmxValue(Lamp1Channel, Value);
            }
        }
    }
}
