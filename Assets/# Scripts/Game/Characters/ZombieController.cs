using UnityEngine;
using Zenject;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class ZombieController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _health;
    [SerializeField] private int _speed;

    [Header("Binding")]
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rb;


    public event Action<ZombieController> OnDead;

    public bool WalkIsOn { get; set; } = true;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _animator?.SetBool("Idle", false);
        _animator?.SetBool("Walk", WalkIsOn);
    }

    private void FixedUpdate()
    {
        if (WalkIsOn) _rb.linearVelocityY = _speed * Time.fixedDeltaTime;
        else _rb.linearVelocityY = 0;
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
}
