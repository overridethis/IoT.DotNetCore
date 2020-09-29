using System;
using System.Drawing;
using System.Threading.Tasks;
using IoT.DotNetCore.BuildMonitor.Contracts;

namespace IoT.DotNetCore.BuildMonitor
{
    public interface IMonitorHardwareRunner
    {
        void Init(Action onPressed);

        void Display(Run run);
        void Beep();
    }
}