using UnityEngine;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    [Inject] private SceneLoadingService sceneLoadingService;

    public void LoadSceneAsync(string sceneName)
    {
        sceneLoadingService.LoadSceneAsync(sceneName, requireUserConfirmation: true).Forget();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
