using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.DotNetCore.Hardware.Run
{
    static class Program
    { 
        private const string RunModeError = "[RUN] Must provide a valid mode to run. (Button, Led)";
        
        static void Main(string[] args)
        {
            if (!args.Any() && !string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine(RunModeError);
                return;
            }
                
            if (!Enum.TryParse<RunMode>(args[0].Trim(), true, out var runMode))
            {
                Console.WriteLine(RunModeError);
                return;
            }

            Console.WriteLine($"[RUN:{runMode.ToString()}] Running ");

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var runTask = Task.Run(async () =>
            {
                switch (runMode)
                {
                    case RunMode.Button:
                        await new ButtonRunner().RunAsync(23, token);
                        break;
                    case RunMode.Buzzer:
                        await new BuzzerRunner().RunAsync(12, token);
                        break;
                    case RunMode.Lcd:
                        await new LcdRunner().RunAsync(token);
                        break;
                    case RunMode.Led:
                        await new LedRunner().RunAsync(18, token);
                        break;
                    case RunMode.RgbLed:
                        await new RgbLedRunner().RunAsync(new[] { 18,17,22 }, token);
                        break;
                }
            }, token);

            Console.WriteLine($"[RUN:{runMode.ToString()}] Press any key (END)");
            Console.ReadKey();

            tokenSource.Cancel();
            runTask.Wait();
            
            Console.WriteLine($"[RUN:{runMode.ToString()}] Ended ");
        }
    }
}