using TMPro;
using UnityEngine;
using Zenject;
using UniRx;

public class HUDCanvasConttollerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    [Inject] private GameManager _gameManager;


    private void Awake()
    {
        _gameManager.ElapsedTime.Subscribe(OnTimeUpdatedHandler);
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