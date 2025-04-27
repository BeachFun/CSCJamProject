using UnityEngine;
using UniRx;
using Zenject;
using RGames.Core;

public class MusicManager : MonoBehaviour, IManager
{
    [Header("Bindings")]
    [SerializeField] private AudioSource _drums;
    [SerializeField] private AudioSource _bass;
    [SerializeField] private AudioSource _guitar;
    [SerializeField] private AudioSource _synth;


    [Inject] private GameManager _gameManager;

    private float _bassVolume;
    private float _guitarVolume;
    private float _synthVolume;


    public ManagerStatus Status { get; private set; }
    public bool Instr1IsOn { get; private set; }
    public bool Instr2IsOn { get; private set; }
    public bool Instr3IsOn { get; private set; }


    private void Awake()
    {
        _bassVolume = _bass.volume;
        _guitarVolume = _guitar.volume;
        _synthVolume = _synth.volume;

        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler);
    }

    private void Start()
    {
        _bass.volume = 0f;
        _guitar.volume = 0f;
        _synth.volume = 0f;
    }


    public void UpdateState(int intstrumentIndex, bool state)
    {
        if (intstrumentIndex == 1)
        {
            Instr1IsOn = state;
            if (state) _bass.volume = _bassVolume;
            else _bass.volume = 0f;
        }
        if (intstrumentIndex == 2)
        {
            Instr2IsOn = state;
            if (state) _guitar.volume = _guitarVolume;
            else _guitar.volume = 0f;
        }
        if (intstrumentIndex == 3)
        {
            Instr3IsOn = state;
            if (state) _synth.volume = _synthVolume;
            else _synth.volume = 0f;
        }
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Played)
        {
            Status = ManagerStatus.Started;

            _drums.Play();
            if (Instr1IsOn) _guitar.Play();
            if (Instr2IsOn) _bass.Play();
            if (Instr3IsOn) _synth.Play();
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