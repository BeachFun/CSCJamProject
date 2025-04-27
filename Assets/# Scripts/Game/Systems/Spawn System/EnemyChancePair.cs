using System;
using UnityEngine;

[System.Serializable]
public class EnemyChancePair : ICloneable
{
    public int ChancePoints = 10;
    public GameObject EnemyPrefab;

    public object Clone()
    {
        return new EnemyChancePair()
        {
            ChancePoints = this.ChancePoints,
            EnemyPrefab = this.EnemyPrefab
        };
    }
}