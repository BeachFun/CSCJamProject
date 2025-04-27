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


    public ManagerStatus Status { get; private set; }
    public bool Instr1IsOn { get; private set; }
    public bool Instr2IsOn { get; private set; }
    public bool Instr3IsOn { get; private set; }


    private void Awake()
    {
        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler);
    }

    public void UpdateState(int intstrumentIndex, bool state)
    {
        if (intstrumentIndex == 1)
        {
            Instr1IsOn = state;
            if (state) _guitar.Play();
            else _guitar.Pause();
        }
        if (intstrumentIndex == 2)
        {
            Instr2IsOn = state;
            if (state) _bass.Play();
            else _bass.Pause();
        }
        if (intstrumentIndex == 3)
        {
            Instr3IsOn = state;
            if (state) _synth.Play();
            else _synth.Pause();
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