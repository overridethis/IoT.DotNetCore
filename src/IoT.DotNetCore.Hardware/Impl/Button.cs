using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.DotNetCore.Hardware.Impl
{
    public class Button : Hardware, IButton
    {
        private Task _loopTask;
        private bool _loop = true;
        private readonly int _pin;
        private Action _onPressed;

        public Button(int pin)
        {
            _pin = pin;
        }

        protected override void OnDestroy()
        {
            _onPressed = () => { };
            _loop = false;
            _loopTask.Wait();
        }

        protected override void OnInit()
        {
            Controller.OpenPin(_pin, PinMode.Input);
            _loopTask = Task.Run(() =>
            {
                while (_loop)
                {
                    if (Controller.Read(_pin) == PinValue.High)
                    {
                        _onPressed();
                    }
                    Thread.Sleep(500);
                }
            });
        }

        public void OnButtonPressed(Action onPressed) =>
            _onPressed = onPressed ?? (() => { });
    }
}