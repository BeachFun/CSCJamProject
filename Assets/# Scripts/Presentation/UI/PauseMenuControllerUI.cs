using UnityEngine;
using Zenject;

public class PauseMenuControllerUI : MonoBehaviour
{
    [Inject] GameManager _gameManager;
    [Inject] UIService _uiService;

    private void Awake()
    {
        if (_gameManager is null) return;

        _gameManager.OnPlayed += Hide;
        _gameManager.OnPaused += Show;
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

    public void OpenSettings()
    {
        _uiService?.OpenSettings();
    }

    public void Exit()
    {
        _gameManager?.ExitGame();
    }
}
