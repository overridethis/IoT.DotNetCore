using System.Device.Gpio;
using System.Threading;

using IoTBuzzer = Iot.Device.Buzzer.Buzzer;

namespace IoT.DotNetCore.Hardware.Impl
{
    public class Buzzer : Hardware, IBuzzer
    {
        private readonly int _pin;
        private IoTBuzzer _buzzer;

        public Buzzer(int pin)
        {
            _pin = pin;
        }

        protected override void OnDestroy()
        {
        }

        protected override void OnInit()
        {
            _buzzer = new IoTBuzzer(_pin);
        }

        public void Tone(int time)
        {
            _buzzer.PlayTone(440, time);
        }
    }
}