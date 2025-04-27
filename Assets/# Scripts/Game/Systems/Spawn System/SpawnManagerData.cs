using UnityEngine;

[CreateAssetMenu(fileName = "SpawnManagerData", menuName = "Game Data/Spawn Manager Data")]
public class SpawnManagerData : ScriptableObject
{
    public EnemyChancePair[] Enemies;
}
