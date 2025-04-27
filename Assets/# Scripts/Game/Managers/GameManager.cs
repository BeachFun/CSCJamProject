using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ReactiveProperty<float> _elapsedTime = new();
    [SerializeField] private float _winTimeInSeconds = 300;

    [Inject] private InputService _inputService;

    private ReactiveProperty<GameState> _gameState = new();
    private bool _isCounting = true;

    public ReactiveProperty<GameState> CurrentGameState => _gameState;


    public bool TimerIsOn => _isCounting;
    public ReactiveProperty<float> ElapsedTime => _elapsedTime;


    private void Awake()
    {
        _inputService.EscapeIsDown.Subscribe(OnEscapeDownHandler);

        print("Game Manager is initialized");
    }

    private void Start()
    {
        ChangeGameState(_gameState.Value);
        _isCounting = true;

        print("Game Manager is Started");
    }

    private void FixedUpdate()
    {
        if (_isCounting)
        {
            _elapsedTime.Value += Time.fixedDeltaTime;
        }
    }


    public void ChangeGameState(GameState state)
    {
        _gameState.Value = state;

        if (state == GameState.Paused) print("Игра приостановлена");
        if (state == GameState.Played) print("Игра возобновлена");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }


    private void OnEscapeDownHandler(bool isEscapeDown)
    {
        if (!isEscapeDown) return;

        if (_gameState.Value == GameState.Paused)
        {
            _isCounting = true;
            ChangeGameState(GameState.Played);
        }
        else
        {
            _isCounting = false;
            ChangeGameState(GameState.Paused);
        }
    }
}
