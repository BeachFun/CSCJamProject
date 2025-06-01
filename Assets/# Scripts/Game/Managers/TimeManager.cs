using UnityEngine;
using UniRx;
using RGames.Core;

public class TimeManager : ServiceBase
{
    [SerializeField] private ReactiveProperty<float> _elapsedTime = new();

    public ReactiveProperty<float> ElapsedTime => _elapsedTime;
    public bool IsPaused { get; private set; }


    #region [Методы] Инициализация и запуск

    public override void Startup()
    {
        this.status.Value = ServiceStatus.Started;
    }

    #endregion


    private void FixedUpdate()
    {
        if (!IsPaused)
        {
            _elapsedTime.Value += Time.fixedDeltaTime;
        }
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
    }
}