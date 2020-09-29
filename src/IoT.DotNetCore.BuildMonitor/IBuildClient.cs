using System.Threading.Tasks;
using IoT.DotNetCore.BuildMonitor.Contracts;

namespace IoT.DotNetCore.BuildMonitor
{
    public interface IBuildClient
    {
        Task<Runs> GetStatusAsync();
    }
}