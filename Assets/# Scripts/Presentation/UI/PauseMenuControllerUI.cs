using UnityEngine;
using Zenject;

public class PauseMenuControllerUI : MonoBehaviour
{
    [Inject] GameManager gameManager;
    [Inject] UIService uiService;

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
