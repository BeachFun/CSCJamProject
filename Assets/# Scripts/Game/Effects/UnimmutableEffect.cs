using UnityEngine;

public class UnimmutableEffect : Effect
{
    [SerializeField] private bool isUnimmutable = true;

    protected override void Use(ZombieController zombie)
    {
        if (zombie is null) return;
        zombie.IsImmutalbe = isUnimmutable;
    }
}
