using System;

namespace IoT.DotNetCore.Hardware
{
    public interface IButton : IHardware
    {
        void OnButtonPressed(Action onPressed);
    }
}