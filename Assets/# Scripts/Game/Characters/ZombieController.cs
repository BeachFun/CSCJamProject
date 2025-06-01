using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class ZombieController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _health;
    [SerializeField] private int _speed;
    [SerializeField] private bool _isImmutalbe;

    [Header("Binding")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    [Header("References")]
    [SerializeField] private AudioClip _clipSpawn;
    [SerializeField] private AudioClip _clipAppear;
    [SerializeField] private AudioClip _clipKill;

    private Rigidbody2D _rb;

    public event Action<ZombieController> OnDead;

    public bool WalkIsOn { get; set; } = true;
    public bool IsSlowed { get; set; }
    public bool IsImmutalbe
    {
        get => _isImmutalbe;
        set => _isImmutalbe = value;
    }
    public int Speed
    {
        get => _speed;
        set => _speed = value;
    }
    public int Road { get; set; }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _animator?.SetBool("Idle", false);
        _animator?.SetBool("Walk", WalkIsOn);

        PlayClip(_clipSpawn);
    }

    private void FixedUpdate()
    {
        if (WalkIsOn) _rb.linearVelocityY = _speed * Time.fixedDeltaTime;
        else _rb.linearVelocityY = 0;
    }


    public void Damage(int point)
    {
        if (IsImmutalbe) return;

        _health -= point;

        if (_health <= 0)
        {
            Kill();
            return;
        }
        else
        {
            // визуальный эффект получения урона
        }
    }

    public async void Kill()
    {
        _health = 0;

        _animator?.SetBool("Dieth", true);
        WalkIsOn = false;
        PlayClip(_clipKill);

        await UniTask.Delay(1000); // TODO: Настроить под конец анимации

        Destroy(this.gameObject);
    }

    private void PlayClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void YouAreAppear()
    {
        PlayClip(_clipAppear);
    }
}
