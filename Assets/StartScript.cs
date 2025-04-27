using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void OnPlayButton1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnPlayButton2()
    {
        SceneManager.LoadScene("Level 2");
    }
}
