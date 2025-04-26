using UnityEngine;
using UniRx;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ReactiveProperty<int> _health;

    public ReactiveProperty<int> Health => _health;


    public void Heat(int points)
    {
        _health.Value -= points;
    }

    public void Heal(int points)
    {
        _health.Value += points;
    }
}