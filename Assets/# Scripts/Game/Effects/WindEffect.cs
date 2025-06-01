using DG.Tweening;
using UnityEngine;

public class WindEffect : Effect
{
    [SerializeField] private int power;
    [SerializeField] private float duration;

    protected override void Use(ZombieController zombie)
    {
        if (zombie is null) return;

        Vector3 pos = zombie.transform.position;
        float endValue = pos.y - power * duration;
        zombie.transform.DOMoveY(endValue, duration);
    }
}