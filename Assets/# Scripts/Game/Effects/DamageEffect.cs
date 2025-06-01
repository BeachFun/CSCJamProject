using UnityEngine;

public class DamageEffect : Effect
{
    [SerializeField] private int damage;

    protected override void Use(ZombieController zombie)
    {
        if (zombie is null) return;
        zombie.Damage(damage);
    }
}
