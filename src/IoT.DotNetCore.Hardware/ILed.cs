namespace  IoT.DotNetCore.Hardware
{
    public interface ILed : IHardware
    {
        bool IsOn { get; }
        
        void On();

        void Off();
    }
}