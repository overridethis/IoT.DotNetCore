using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IoT.DotNetCore.Hardware.Impl;

namespace IoT.DotNetCore.Hardware.Run
{
    public class RgbLedRunner
    {
        public Task RunAsync(int[] pin, CancellationToken token)
        {
            var rgb = new RgbLed(pin[0],pin[1],pin[2]);
            rgb.Init();

            return Task.Run(() =>
            {
                while (true)
                {

                    rgb.SetColor(Color.Red);
                    Thread.Sleep(1000);
                    
                    rgb.SetColor(Color.Green);
                    Thread.Sleep(1000);
                    
                    rgb.SetColor(Color.Blue);
                    Thread.Sleep(1000);

                    rgb.SetColor(Color.Yellow);
                    Thread.Sleep(1000);

                    if (token.IsCancellationRequested)
                    {
                        rgb.Off();
                        break;
                    }
                }
            }, token);
        }
    }
}
