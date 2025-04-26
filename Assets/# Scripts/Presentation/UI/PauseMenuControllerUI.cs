using UnityEngine;
using Zenject;

public class PauseMenuControllerUI : MonoBehaviour
{
    [Inject] GameManager gameManager;
    [Inject] UIService uiService;

    private void Awake()
    {
        if (gameManager is null) return;

        gameManager.OnPlayed += Hide;
        gameManager.OnPaused += Show;
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
        gameManager?.ChangeGameState(GameState.Played);
    }

    public void OpenSettings()
    {
        uiService?.OpenSettings();
    }

    public void Exit()
    {
        gameManager?.ExitGame();
    }
}
