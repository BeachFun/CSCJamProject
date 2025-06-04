using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingService
{
    private readonly ReactiveProperty<bool> isLoading = new();
    private readonly ReactiveProperty<float> progress = new();
    private readonly ReactiveProperty<bool> isLoadedForConfirm = new();

    private AsyncOperation currentLoadOp;
    private bool requireConfirmation;
    private bool confirmed;

    public ReadOnlyReactiveProperty<bool> IsLoading => isLoading.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<float> Progress => progress.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<bool> IsLoadedForConfirm => isLoadedForConfirm.ToReadOnlyReactiveProperty();


    /// <summary>
    /// Запускает загрузку сцены. Если requireUserConfirmation = true — ждёт вызова ConfirmActivation().
    /// </summary>
    public async UniTaskVoid LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, bool requireUserConfirmation = false)
    {
        isLoading.Value = true;
        isLoadedForConfirm.Value = false;
        progress.Value = 0f;
        requireConfirmation = requireUserConfirmation;
        confirmed = false;

        await SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

        currentLoadOp = SceneManager.LoadSceneAsync(sceneName, mode);
        currentLoadOp.allowSceneActivation = false;

        while (currentLoadOp.progress < 0.9f)
        {
            progress.Value = currentLoadOp.progress;
            await UniTask.Yield();
        }

        progress.Value = 1f;

        if (!requireConfirmation)
        {
            currentLoadOp.allowSceneActivation = true;
            await currentLoadOp;
            await SceneManager.UnloadSceneAsync("Loading");
            isLoading.Value = false;
        }
        else
        {
            isLoadedForConfirm.Value = true;
        }
    }

    /// <summary>
    /// Подтверждает завершение загрузки и активирует сцену.
    /// </summary>
    public async UniTask ConfirmActivation()
    {
        if (!requireConfirmation || currentLoadOp == null || confirmed)
            return;

        confirmed = true;
        currentLoadOp.allowSceneActivation = true;
        await currentLoadOp;
        await SceneManager.UnloadSceneAsync("Loading");
        isLoading.Value = false;
    }
}
