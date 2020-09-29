using System;
using System.Threading;
using System.Threading.Tasks;
using IoT.DotNetCore.Hardware.Impl;

namespace IoT.DotNetCore.Hardware.Run
{
    public class ButtonRunner
    {
        public Task RunAsync(int pin, CancellationToken token)
        {
            return Task.Run(() =>
            {
                var button = new Button(pin);
                button.Init();
                button.OnButtonPressed(() =>
                {
                    Console.WriteLine($"[RUN:Button] Button on {pin} has been pressed");
                });

                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, token);
        }
    }
}