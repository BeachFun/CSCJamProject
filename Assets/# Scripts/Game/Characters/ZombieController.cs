using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using RGames.Core;

[RequireComponent(typeof(Rigidbody2D))]
public class ZombieController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _health;
    [SerializeField] private int _speed;

    [Header("Binding")]
    [SerializeField] private Animator _animator;

    private GameManager _gameManager;
    private bool _walkIsOn = true;
    private Rigidbody2D _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler);
    }

    private void Start()
    {
        _animator?.SetBool("Idle", false);
        _animator?.SetBool("Walk", _walkIsOn);
    }

    private void FixedUpdate()
    {
        if (_walkIsOn) _rb.linearVelocityY = _speed * Time.fixedDeltaTime;
    }


    public void Damage(int point)
    {
        if (point < 0)
        {
            Kill();
            return;
        }

        _health -= point;
    }

    public async void Kill()
    {
        _health = 0;

        _animator?.SetBool("Dieth", true);

        await UniTask.Delay(1000);

        Destroy(this.gameObject);
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Played)
        {
            _walkIsOn = true;
        }
        else
        {
            _walkIsOn = false;
        }
    }

}
