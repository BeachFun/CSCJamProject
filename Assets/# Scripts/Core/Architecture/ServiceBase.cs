using UniRx;
using UnityEngine;

namespace RGames.Core
{
    public abstract class ServiceBase : MonoBehaviour, IService
    {
        protected ReactiveProperty<ServiceStatus> status = new();

        public ReadOnlyReactiveProperty<ServiceStatus> Status => status.ToReadOnlyReactiveProperty();

        /// <summary>
        /// Запуск сервиса
        /// </summary>
        public abstract void Startup();

        protected virtual void Awake()
        {
            Status.Subscribe(OnStatusUpdated).AddTo(this);
            Startup();
        }

        private void OnStatusUpdated(ServiceStatus status)
        {
            print($"{name} is {status}");
        }
    }
}
