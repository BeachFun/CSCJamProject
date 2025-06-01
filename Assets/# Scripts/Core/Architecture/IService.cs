using UniRx;

namespace RGames.Core
{
    public interface IService
    {
        ReadOnlyReactiveProperty<ServiceStatus> Status { get; }
        void Startup();
    }

    public enum ServiceStatus
    {
        Shutdown,
        Initializing,
        Started,
        Suspended
    }
}
