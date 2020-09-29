using System.Device.I2c;
using System.Device.Gpio;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;

namespace IoT.DotNetCore.Hardware.Impl
{
    public class Lcd : Hardware, ILcd
    {
        private Lcd1602 _lcd;

        protected override void OnDestroy()
        {
            _lcd.BacklightOn = false;
            _lcd.DisplayOn = false;
        }

        protected override void OnInit()
        {            
            var driver = new Pcf8574(I2cDevice.Create(new I2cConnectionSettings(busId: 1, deviceAddress: 0x27)));
            _lcd = new Lcd1602(
                registerSelectPin: 0,
                enablePin: 2,
                dataPins: new int[] {4, 5, 6, 7},
                backlightPin: 3,
                readWritePin: 1,
                controller: new GpioController(PinNumberingScheme.Logical, driver))
            {
                BacklightOn = true,
                DisplayOn = true,
            };
        }

        public void Write(int line, string message)
        {
            _lcd.SetCursorPosition(0,line);
            _lcd.Write(message);
        }

        public void Clear() => _lcd.Clear();
    }
}