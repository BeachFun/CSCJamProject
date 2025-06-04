using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingService
{
    // TODO: доработать

    private ReactiveProperty<bool> isLoading = new();
    private ReactiveProperty<float> progress = new();
    private ReactiveProperty<bool> isLoaded = new();

    public ReadOnlyReactiveProperty<bool> IsLoading => isLoading.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<float> Progress => progress.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<bool> IsLoaded => isLoaded.ToReadOnlyReactiveProperty();


    public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(sceneName, mode);

        //UniTask.Run();
    }

    private void ReadProgress(AsyncOperation operation)
    {

    }
}