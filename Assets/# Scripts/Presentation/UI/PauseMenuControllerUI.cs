using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using UniRx;

public class PauseMenuControllerUI : MonoBehaviour
{
    [Inject] GameManager _gameManager;
    [Inject] UIService _uiService;


    private void Awake()
    {
        if (_gameManager is null) return;

        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler);
    }


    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }


    public void Resume()
    {
        _gameManager?.ChangeGameState(GameState.Played);
    }

    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void OpenSettings()
    {
        _uiService?.OpenSettings();
    }

    public void Exit()
    {
        _gameManager?.ExitGame();
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Paused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
