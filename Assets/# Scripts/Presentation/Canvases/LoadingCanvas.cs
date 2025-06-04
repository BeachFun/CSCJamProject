using Zenject;
using UniRx;
using UnityEngine;

/// <summary>
/// Менеджер управления загрузкой
/// </summary>
public class LoadingCanvas : CanvasBase
{
    // TODO: доработать

    [Header("Settings")]
    [SerializeField] private bool dontDestroyOnLoad = true;
    [Header("Bindings")]
    [SerializeField] private LoadingScreen screen;

    [Inject] private LoadingService loadingService;

    private void Awake()
    {
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);

        loadingService.IsLoading
            .Subscribe(Show)
            .AddTo(this);

        loadingService.Progress
            .Subscribe(screen.UpdateData)
            .AddTo(this);
    }
}