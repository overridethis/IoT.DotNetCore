using System.Device.Gpio;
    
namespace IoT.DotNetCore.Hardware.Impl
{
    public class Led : Hardware, ILed
    {
        private readonly int _pin;

        public Led(int pin)
        {
            _pin = pin;
        }

        protected override void OnDestroy()
        {
            Controller.ClosePin(_pin);
        }

        protected override void OnInit()
        {
            Controller.OpenPin(_pin);
            Controller.SetPinMode(_pin, PinMode.Output);
        }

        public bool IsOn => Controller.Read(_pin) == PinValue.High;
        
        public void On() => Controller.Write(_pin, PinValue.High);

        public void Off() => Controller.Write(_pin, PinValue.Low);
    }
}