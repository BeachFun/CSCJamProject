using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ZombieController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _health;
    [SerializeField] private int _speed;

    [Header("Binding")]
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _animator?.SetBool("Idle", false);
        _animator?.SetBool("Walk", true);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityY = _speed * Time.fixedDeltaTime;
    }


    public void Damage(int point)
    {
        if (point < 0) return;

        _health -= point;
    }

    public void Kill()
    {
        _health = 0;

        _animator?.SetBool("Dieth", true);
    }
}
