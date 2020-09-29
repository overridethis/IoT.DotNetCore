using System.Device.Gpio;

namespace IoT.DotNetCore.Hardware.Impl
{
    public abstract class Hardware : IHardware
    {
        protected GpioController Controller { get; private set; }

        public bool IsDestroyed { get; private set; } = false;
        
        public bool IsInitialized { get; private set; } = false;
        
        public void Init()
        {
            Controller = new GpioController();
            OnInit();
            this.IsInitialized = true;
        }

        public void Destroy()
        {
            OnDestroy();
            Controller.Dispose();
            this.IsDestroyed = true;
        }

        protected abstract void OnDestroy();

        protected abstract void OnInit();
    }
}