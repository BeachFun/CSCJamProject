using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using System;
using System.Threading;
using RGames.Core;
using Zenject;

public class Instrument : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private float staminaRate = 1f;
    [SerializeField] private int reload = 10;
    [SerializeField] private float reloadRate = 1f;
    [Tooltip("Расход за применение")]
    [SerializeField] private int consumption = 1;
    [SerializeField] private InstrumentMode mode = InstrumentMode.Manual;
    [Header("References"), Space]
    [SerializeField] private GameObject soundPrefab;

    private ReactiveProperty<int> m_stamina;
    private ReactiveProperty<float> m_staminaPrecent = new();
    private ReactiveProperty<bool> m_available = new();
    private readonly CompositeDisposable m_disposables = new();
    private CancellationTokenSource m_playCts;
    private CancellationTokenSource m_reloadCts;
    [Inject] private GameManager _gameManager;

    private bool m_isPlaying;

    public ReadOnlyReactiveProperty<int> Stamina => m_stamina.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<float> StaminaPrecent => m_staminaPrecent.ToReadOnlyReactiveProperty();
    public ReadOnlyReactiveProperty<bool> IsAvailable => m_available.ToReadOnlyReactiveProperty();
    public ServiceStatus Status { get; private set; } = ServiceStatus.Started;
    public InstrumentMode Mode => mode;


    private void Awake()
    {
        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler);

        m_stamina = new ReactiveProperty<int>(maxStamina);

        // Clamp stamina to range [0, maxStamina]
        m_stamina
            .Subscribe(value =>
            {
                if (value < 0)
                    m_stamina.Value = 0;
                else if (value > maxStamina)
                    m_stamina.Value = maxStamina;
            })
            .AddTo(m_disposables);

        m_stamina
            .Where(x => x <= 0 && m_isPlaying)
            .Subscribe(_ => Stop())
            .AddTo(m_disposables);

        m_stamina.Subscribe(OnStaminaUpdateHandler);
    }

    private void Start()
    {
        StartReloadingLoop().Forget();
    }

    [ContextMenu("Start")]
    public void Play()
    {
        if (m_isPlaying || m_stamina.Value < consumption || Status != ServiceStatus.Started) return;

        m_isPlaying = true;
        m_playCts?.Cancel();
        m_playCts = new CancellationTokenSource();

        if (mode == InstrumentMode.Auto)
        {
            PlayAutoAsync(m_playCts.Token).Forget();
        }
        else
        {
            PlayOnceAsync(m_playCts.Token).Forget();
        }
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        m_isPlaying = false;
        m_playCts?.Cancel();
    }

    private async UniTaskVoid PlayAutoAsync(CancellationToken token)
    {
        try
        {
            while (m_isPlaying && m_stamina.Value >= consumption)
            {
                PlayInternal();
                await UniTask.Delay(TimeSpan.FromSeconds(staminaRate), cancellationToken: token);
            }
        }
        catch (OperationCanceledException) { }
        finally
        {
            m_isPlaying = false;
        }
    }

    private async UniTaskVoid PlayOnceAsync(CancellationToken token)
    {
        try
        {
            PlayInternal();
            await UniTask.Delay(TimeSpan.FromSeconds(staminaRate), cancellationToken: token);
        }
        catch (OperationCanceledException) { }
        finally
        {
            m_isPlaying = false;
        }
    }

    private void PlayInternal()
    {
        if (m_stamina.Value >= consumption)
        {
            m_stamina.Value -= consumption;
            if (soundPrefab != null)
            {
                Instantiate(soundPrefab, transform);
            }
        }
        else
        {
            Stop();
        }
    }

    private async UniTaskVoid StartReloadingLoop()
    {
        m_reloadCts = new CancellationTokenSource();
        var token = m_reloadCts.Token;

        try
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay((int)reloadRate * 1000, cancellationToken: token);
                await UniTask.WaitWhile(() => Status != ServiceStatus.Started);

                if (!m_isPlaying && m_stamina.Value < maxStamina)
                {
                    m_stamina.Value += reload;
                }
            }
        }
        catch (OperationCanceledException) { }
    }

    private void OnDestroy()
    {
        m_playCts?.Cancel();
        m_reloadCts?.Cancel();
        m_disposables.Dispose();
    }


    private void OnStaminaUpdateHandler(int stamina)
    {
        m_available.Value = stamina >= consumption;
        m_staminaPrecent.Value = (float)stamina / maxStamina;
    }

    private void OnGameStateChangedHandler(GameState state)
    {
        Status = state == GameState.Played
           ? ServiceStatus.Started
           : ServiceStatus.Suspended;
    }

}

public enum InstrumentMode
{
    Auto,
    Manual
}
