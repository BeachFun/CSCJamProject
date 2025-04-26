using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_UI : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
