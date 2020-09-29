using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using IoT.DotNetCore.BuildMonitor.Contracts;
using IoT.DotNetCore.Hardware;

namespace IoT.DotNetCore.BuildMonitor.Impl
{
    public class MonitorHardwareRunner : IMonitorHardwareRunner
    {
        private readonly IList<IHardware> _hardwares = new List<IHardware>();
        private readonly IButton _button;
        private readonly IBuzzer _buzzer;
        private readonly ILcd _lcd;
        private readonly ILed _led;

        private Task _runTask;
        private CancellationTokenSource _tokenSource;

        public MonitorHardwareRunner(
            IButton button,
            IBuzzer buzzer,
            ILcd lcd,
            ILed led)
        {
            _hardwares.Add(_button = button);
            _hardwares.Add(_buzzer = buzzer);
            _hardwares.Add(_lcd = lcd);
            _hardwares.Add(_led = led);
        }

        public void Init(Action onPressed)
        {
            foreach(var hardware in _hardwares)
                hardware.Init();

            _button.OnButtonPressed(onPressed);
        }

        public void Display(Run run)
        {
            Task.Run(async () =>
            {
                if (_runTask != null)
                {
                    _tokenSource.Cancel();
                    await _runTask;
                }

                _tokenSource = new CancellationTokenSource();
                _runTask = Task.Run(() =>
                {
                    while (true)
                    {
                        if (run == null || run.IsFailure())
                            RunFailed();
                        else if (run.IsCompleted())
                            RunCompleted();
                        else if (run.IsQueued())
                            RunQueued();
                        
                        Thread.Sleep(250);
                        
                        if (_tokenSource.Token.IsCancellationRequested)
                        {
                            _lcd.Clear();
                            _led.Off();
                            break;
                        }    
                    }
                    
                }, _tokenSource.Token);
            });
        }

        public void Beep()
        {
            _buzzer.Tone(250);  
        } 

        private void RunFailed()
        {
            _lcd.Write(0, "Failed.");
            _lcd.Write(1, DateTime.Now.ToString("h:mm:ss tt zz"));
            _led.On();
        }

        private void RunQueued()
        {          
            _lcd.Write(0, "Queued.");
            _lcd.Write(1, DateTime.Now.ToString("h:mm:ss tt zz"));
            _led.On();
            Thread.Sleep(250);
            _led.Off();
            Thread.Sleep(250);
        }

        private void RunCompleted()
        {
            _lcd.Write(0, "Completed.");
            _lcd.Write(1, DateTime.Now.ToString("h:mm:ss tt zz"));
            _led.Off();
        }
    }
}