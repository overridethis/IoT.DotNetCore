using System.Threading;
using System.Threading.Tasks;
using IoT.DotNetCore.Hardware.Impl;

namespace IoT.DotNetCore.Hardware.Run
{
    public class BuzzerRunner
    {
        public Task RunAsync(int pin, CancellationToken token)
        {
            var buzzer = new Buzzer(pin);
            buzzer.Init();
            
            return Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    buzzer.Tone(1000);
                    
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }    
                }
            }, token);
        }
    }
}