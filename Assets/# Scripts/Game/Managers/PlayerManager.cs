using UnityEngine;
using UniRx;
using RGames.Core;

public class PlayerManager : ServiceBase
{
    [SerializeField] private ReactiveProperty<int> _health;

    public ReadOnlyReactiveProperty<int> Health => _health.ToReadOnlyReactiveProperty();


    #region [Методы] Инициализация и запуск

    public override void Startup()
    {
        this.status.Value = ServiceStatus.Started;
    }

    #endregion


    public void Heat(int points)
    {
        _health.Value -= points;
    }

    public void Heal(int points)
    {
        _health.Value += points;
    }
}
