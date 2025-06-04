using UnityEngine;
using UniRx;
using Zenject;
using System.Collections;
using Cysharp.Threading.Tasks;

/// <summary>
/// Менеджер управления отображением загрузки на сцене Loading
/// </summary>
public class LoadingSceneCanvas : CanvasBase
{
    [Header("Bindings")]
    [SerializeField] private LoadingScreen screen;

    [Inject] private SceneLoadingService loadingService;

    private void Awake()
    {
        loadingService.IsLoading
            .Subscribe(screen.Show)
            .AddTo(this);

        loadingService.Progress
            .Subscribe(screen.UpdateData)
            .AddTo(this);

        loadingService.IsLoadedForConfirm
            .Where(isShow => isShow == true)
            .Subscribe(x => ConfirmShow())
            .AddTo(this);
    }

    private void ConfirmShow()
    {
        screen.ShowConfirmLabel(true);
        WaitForAnyKey(loadingService).Forget();
    }

    public static async UniTaskVoid WaitForAnyKey(SceneLoadingService service)
    {
        await UniTask.WaitUntil(() => Input.anyKeyDown);
        service.ConfirmActivation();
    }
}