using UnityEngine;
using UniRx;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ReactiveProperty<int> _heatPoints;

    public ReactiveProperty<int> Health => _heatPoints;


    public void Heat(int points)
    {
        _heatPoints.Value -= points;
    }

    public void Heal(int points)
    {
        _heatPoints.Value += points;
    }
}