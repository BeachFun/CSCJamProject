using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }
}
