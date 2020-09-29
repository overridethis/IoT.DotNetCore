using System.Threading;
using System.Threading.Tasks;
using IoT.DotNetCore.Hardware.Impl;

namespace IoT.DotNetCore.Hardware.Run
{
    public class LedRunner
    { 
        public Task RunAsync(int pin, CancellationToken token)
        {
            var led = new Led(pin);
            led.Init();

            return Task.Run(() =>
            {
                while (true)
                {
                    if (led.IsOn)
                        led.Off();
                    else
                        led.On();
                    Thread.Sleep(1000);

                    if (token.IsCancellationRequested)
                    {
                        led.Off();
                        break;
                    }
                }
            }, token);
        }
    }
}