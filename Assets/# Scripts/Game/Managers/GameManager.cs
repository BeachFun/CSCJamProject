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
        ChangeGameState(GameState.Played);

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
        if (state == GameState.Paused)
        {
            _isCounting = false;
            print("Игра приостановлена");
        }
        else
        {
            _isCounting = true;
            print("Игра возобновлена");
        }

        _gameState.Value = state;
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
            ChangeGameState(GameState.Played);
        }
        else
        {
            ChangeGameState(GameState.Paused);
        }
    }
}
