using UnityEngine;
using Zenject;
using UniRx;

public class PauseMenuControllerUI : MonoBehaviour
{
    [Inject] GameManager _gameManager;
    [Inject] GameoverManager _gameoverManager;
    [Inject] UIService _uiService;

    private bool _lock;

    private void Awake()
    {
        if (_gameManager is null || _uiService is null)
        {
            GameManagersInstaller.DiContainer.Inject(this);
        }

        _gameManager.CurrentGameState
            .Subscribe(OnGameStateChangedHandler)
            .AddTo(this);

        _gameoverManager.State
            .Where(x => x != GameoverState.None)
            .Subscribe((x) =>
            {
                _lock = true;
                Hide();
            })
            .AddTo(this);
    }


    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }


    public void Resume()
    {
        _gameManager?.ChangeGameState(GameState.Played);
    }

    public void RestartCurrentScene()
    {
        _gameManager?.Restart();
    }

    public void Exit()
    {
        _gameManager?.Exit();
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        if (_lock) return;

        if (state == GameState.Paused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
