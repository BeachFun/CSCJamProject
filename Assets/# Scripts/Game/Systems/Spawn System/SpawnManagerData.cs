using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnManagerData", menuName = "Game Data/Spawn Manager Data")]
public class SpawnManagerData : ScriptableObject, ICloneable
{
    public EnemyChancePair[] Enemies;

    public object Clone()
    {
        return new SpawnManagerData()
        {
            Enemies = this.Enemies.Select(x => (EnemyChancePair)x.Clone()).ToArray()
        };
    }
}
