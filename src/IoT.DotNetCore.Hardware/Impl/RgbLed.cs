using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.Pwm;
using System.Device.Pwm.Drivers;
using System.Drawing;

namespace IoT.DotNetCore.Hardware.Impl
{
    public class RgbLed : Hardware, IRgbLed
    {
        private readonly int[] pins;
        private readonly IList<PwmChannel> channels = new List<PwmChannel>();
        private Color color;

        public RgbLed(
            int redPin,
            int greenPin,
            int bluePin)
        {
            this.pins = new[] { redPin, greenPin, bluePin };
        }

        public bool IsOff => color == Color.FromArgb(0, 0, 0);

        public void Off() => SetColor(Color.FromArgb(0, 0, 0));

        public void SetColor(Color color)
        {
            this.color = color;
            this.channels[0].DutyCycle = MapDutyCycle(color.R);
            this.channels[1].DutyCycle = MapDutyCycle(color.G);
            this.channels[2].DutyCycle = MapDutyCycle(color.B);
        }

        private double MapDutyCycle(double number, double in_min = 0, double in_max = 255, double out_min = 0, double out_max = 1)
            => (number - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

        protected override void OnDestroy()
        {
            SetColor(Color.FromArgb(0, 0, 0));
            foreach(var channel in channels)
            {
                channel.Stop();
            }
        }

        protected override void OnInit()
        {
            foreach(var pin in pins)
            {
                var pwm = new SoftwarePwmChannel(pin)
                {
                    Frequency = 2000
                };
                pwm.DutyCycle = 0;
                pwm.Start();
                channels.Add(pwm);
            }
        }
    }
}
