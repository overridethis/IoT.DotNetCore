namespace IoT.DotNetCore.Hardware
{
    public interface ILcd : IHardware
    {
        void Write(int line, string message);
        void Clear();
    }
}