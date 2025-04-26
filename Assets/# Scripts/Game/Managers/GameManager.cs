using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _gameState;


    public event Action OnPlayed;
    public event Action OnPaused;


    private void Start()
    {
        ChangeGameState(_gameState);

        Debug.Log("Game Manager is Started");
    }


    public void ChangeGameState(GameState state)
    {
        _gameState = state;

        switch (state)
        {
            case GameState.Paused:
                Debug.Log("Игра приостановлена");
                OnPlayed?.Invoke();
                break;
            case GameState.Played:
                Debug.Log("Игра возобновлена");
                OnPlayed?.Invoke();
                break;

            default: break;
        }
    }

    public void ExitGame()
    {

    }
}
