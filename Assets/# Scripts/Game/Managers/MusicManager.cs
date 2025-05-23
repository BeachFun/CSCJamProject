using UnityEngine;
using UniRx;
using Zenject;
using RGames.Core;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour, IManager
{
    [Header("Bindings")]
    [SerializeField] private AudioSource _drums;
    [SerializeField] private AudioSource _bass;
    [SerializeField] private AudioSource _guitar;
    [SerializeField] private AudioSource _synth;

    [Inject] private GameManager _gameManager;

    public ManagerStatus Status { get; private set; }


    private void Awake()
    {
        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler).AddTo(this);
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Played)
        {
            Status = ManagerStatus.Started;

            _drums.Play();
            _guitar.Play();
            _bass.Play();
            _synth.Play();
        }
        else
        {
            Status = ManagerStatus.Suspended;

            _drums.Pause();
            _guitar.Pause();
            _bass.Pause();
            _synth.Pause();
        }
    }
}