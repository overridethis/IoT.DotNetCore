namespace IoT.DotNetCore.Hardware
{
    public interface IHardware
    {
        bool IsDestroyed { get; }
        bool IsInitialized { get; }
        
        void Init();
        void Destroy();
    }
}