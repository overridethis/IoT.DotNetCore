using System;
using System.Threading;
using System.Threading.Tasks;
using IoT.DotNetCore.Hardware.Impl;

namespace IoT.DotNetCore.Hardware.Run
{
    public class LcdRunner
    {
        public Task RunAsync(CancellationToken token)
        {
            return Task.Run(() => 
            {
                var lcd = new Lcd();
                lcd.Init();

                while (true)
                {
                    lcd.Write(0, "Hello World!");
                    lcd.Write(1, DateTime.Now.ToString("G"));
                    Thread.Sleep(1000);

                    if (token.IsCancellationRequested)
                    {
                        lcd.Write(0, "The End!");
                        lcd.Destroy();
                        break;
                    }
                }
            }, token);
        }
    }
}