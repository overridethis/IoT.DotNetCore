namespace IoT.DotNetCore.BuildMonitor
{
    public interface IMonitorConfiguration
    {
        string Owner { get; }
        string Repo { get; }
        string Token { get; }
    }
}