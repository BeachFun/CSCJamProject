using UnityEngine.SceneManagement;
using UniRx;
using Zenject;
using RGames.Core;

public class GameManager : ServiceBase
{
    [Inject] private InputService _inputService;
    [Inject] private TimeManager _timeManager;
    [Inject] private GameoverManager _gameoverManager;
    private readonly ReactiveProperty<GameState> _gameState = new();

    public ReactiveProperty<GameState> CurrentGameState => _gameState;


    #region [Методы] Инициализация и запуск

    public override void Startup()
    {
        _inputService.EscapeIsDown
            .Subscribe((isDown) => { if (isDown) SwitchGameState(); })
            .AddTo(this);

        _gameoverManager.State
            .Where(x => x != GameoverState.None)
            .Subscribe(x => _gameState.Value = GameState.Paused)
            .AddTo(this);

        this.status.Value = ServiceStatus.Started;
    }

    private void Start()
    {
        ChangeGameState(GameState.Played);
    }

    #endregion


    public void ChangeGameState(GameState state)
    {
        if (state == GameState.Paused)
        {
            _timeManager.Pause();
            print("Игра приостановлена");
        }
        else
        {
            _timeManager.Resume();
            print("Игра возобновлена");
        }

        _gameState.Value = state;
    }

    public void SwitchGameState()
    {
        if (_gameState.Value == GameState.Paused)
        {
            ChangeGameState(GameState.Played);
        }
        else
        {
            ChangeGameState(GameState.Paused);
        }
    }

    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(currentScene.name);
    }

    public void Exit()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}

public enum GameState
{
    Played,
    Paused
}
