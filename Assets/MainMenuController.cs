using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    [Inject] private UIService _uiService;

    public void OnPlayButton1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnPlayButton2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void OnSettingsButton()
    {
        _uiService?.OpenScreen("Settings");
    }

    public void OnCreditsButton()
    {
        _uiService?.OpenScreen("References");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
