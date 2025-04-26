using System;
using UnityEngine;
using Zenject;
using UniRx;

public class GameManager : MonoBehaviour
{
    [Inject] private InputService _inputService;

    private GameState _gameState;


    public event Action OnPlayed;
    public event Action OnPaused;


    public GameManager()
    {
        print("GameManager class is initialized");
    }

    private void Awake()
    {
        _inputService.EscapeIsDown.Subscribe(OnEscapeDownHandler);

        print("Game Manager is initialized");
    }

    private void Start()
    {
        ChangeGameState(_gameState);

        print("Game Manager is Started");
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
