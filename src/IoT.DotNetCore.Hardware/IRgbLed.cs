using System;
using System.Drawing;

namespace IoT.DotNetCore.Hardware
{
    public interface IRgbLed : IHardware
    {
        void SetColor(Color color);

        bool IsOff { get; }

        void Off();
    }
}
