using System;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ReactiveProperty<float> _elapsedTime = new();
    [SerializeField] private float _winTimeInSeconds = 300;

    [Inject] private InputService _inputService;

    private GameState _gameState;
    private bool _isCounting = true;

    public event Action OnPlayed;
    public event Action OnPaused;


    public bool TimerIsOn => _isCounting;
    public ReactiveProperty<float> ElapsedTime => _elapsedTime;


    private void Awake()
    {
        _inputService.EscapeIsDown.Subscribe(OnEscapeDownHandler);

        print("Game Manager is initialized");
    }

    private void Start()
    {
        ChangeGameState(_gameState);
        _isCounting = true;

        print("Game Manager is Started");
    }

    private void FixedUpdate()
    {
        if (_isCounting)
        {
            _elapsedTime.Value += Time.fixedDeltaTime;
        }
    }


    public void ChangeGameState(GameState state)
    {
        _gameState = state;

        switch (state)
        {
            case GameState.Paused:
                print("Игра приостановлена");
                OnPaused?.Invoke();
                break;
            case GameState.Played:
                print("Игра возобновлена");
                OnPlayed?.Invoke();
                break;

            default: break;
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }


    private void OnEscapeDownHandler(bool isEscapeDown)
    {
        if (!isEscapeDown) return;

        if (_gameState == GameState.Paused)
        {
            ChangeGameState(GameState.Played);
        }
        else
        {
            ChangeGameState(GameState.Paused);
        }
    }
}
