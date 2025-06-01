using UnityEngine;
using UniRx;
using Zenject;
using RGames.Core;

public class MusicManager : ServiceBase
{
    [Header("Bindings")]
    [SerializeField] private AudioSource _drums;
    [SerializeField] private AudioSource _bass;
    [SerializeField] private AudioSource _guitar;
    [SerializeField] private AudioSource _synth;

    [Inject] private GameManager _gameManager;


    #region [Методы] Инициализация и запуск

    public override void Startup()
    {
        _gameManager.CurrentGameState
            .Subscribe(OnGameStateChangedHandler)
            .AddTo(this);
    }

    #endregion


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Played)
        {
            this.status.Value = ServiceStatus.Started;

            _drums.Play();
            _guitar.Play();
            _bass.Play();
            _synth.Play();
        }
        else
        {
            this.status.Value = ServiceStatus.Suspended;

            _drums.Pause();
            _guitar.Pause();
            _bass.Pause();
            _synth.Pause();
        }
    }
}