using UnityEngine;
using UniRx;
using TMPro;
using Zenject;

public class HUDCanvasContollerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private HealthIndicator _healthIndicator;

    [Inject] private GameManager _gameManager;
    [Inject] private TimeManager _timeManager;
    [Inject] private PlayerManager _playerManager;

    private void Awake()
    {
        _timeManager.ElapsedTime.Subscribe(OnTimeUpdatedHandler);
        _playerManager.Health.Subscribe(x => _healthIndicator.UpdateHearts(x)).AddTo(this);
    }

    private void Start()
    {
        _healthIndicator.UpdateHearts(_playerManager.Health.Value);
    }

    public void Pause()
    {
        _gameManager?.ChangeGameState(GameState.Paused);
    }

    private void OnTimeUpdatedHandler(float time)
    {
        if (_timerText is null) return;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
