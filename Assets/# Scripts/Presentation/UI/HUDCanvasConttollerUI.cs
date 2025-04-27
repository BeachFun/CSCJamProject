using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Zenject;

public class HUDCanvasConttollerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image[] hearts;

    [Inject] private GameManager _gameManager;
    [Inject] private PlayerManager _playerManager;

    private void Awake()
    {
        _gameManager.ElapsedTime.Subscribe(OnTimeUpdatedHandler);
        _playerManager.Health.Subscribe(UpdateHearts).AddTo(this);
    }

    private void Start()
    {
        UpdateHearts(_playerManager.Health.Value);
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

    private void UpdateHearts(int health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;
                hearts[i].color = Color.white;
            }
            else if (i == health)
            {
                hearts[i].enabled = true;
                hearts[i].color = new Color(1f, 1f, 1f, 0.5f);
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health <= 1)
        {
            Debug.Log("Game Over!");
        }
    }
}