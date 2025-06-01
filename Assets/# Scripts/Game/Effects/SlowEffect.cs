using UnityEngine;

public class SlowEffect : Effect
{
    [SerializeField] private float slowCoef = 1f;

    protected override void Use(ZombieController zombie)
    {
        if (zombie is null) return;

        if (!zombie.IsSlowed)
        {
            zombie.Speed = (int)(zombie.Speed / slowCoef);
            zombie.IsSlowed = true;
        }
    }
}
