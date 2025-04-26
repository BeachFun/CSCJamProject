using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnExitButton()
    {
        Application.Quit();
        Debug.Log("Игра закрыта!");
    }
}
