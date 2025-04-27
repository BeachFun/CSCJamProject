using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Settings - main")]
    [SerializeField] private float _minSpawnIntervalInSeconds;
    [SerializeField] private float _maxSpawnIntervalInSeconds;
    [SerializeField] private float _intervalStepAccelerationSpawn;
    [SerializeField, Range(0.0f, 1.0f)] private float _stepAccelerationTimeInSeconds;
    [SerializeField] private float _timeInSecondsAccelerationStop;

    [Header("Settings - spawn prefabs")]
    [SerializeField] private SpawnManagerData _data;

    [Header("Binding")]
    [SerializeField] private SpawnZone2D[] _spawnZones;


    private float _lastSpawnTime;


    private void Awake()
    {
        _data = (SpawnManagerData)_data.Clone();

        print("Spawn Manager is initialized");
    }

    private void Start()
    {
        print("Spawn Manager is Started");
    }

    private void FixedUpdate()
    {
        _data.Enemies[0].ChancePoints += 1;
    }


    [ContextMenu("Тест - Zombie Spawn")]
    public void Spawn()
    {
        GameObject enemyPrefab = GetRandomEnemy();

        if (enemyPrefab is null) return;

        _spawnZones[Random.Range(0, _spawnZones.Length)].Spawn(enemyPrefab);

        _lastSpawnTime = 0;
    }

    private GameObject GetRandomEnemy()
    {
        int totalChance = 0;

        foreach (var enemy in _data.Enemies)
        {
            totalChance += enemy.ChancePoints;
        }

        int randomPoint = Random.Range(0, totalChance);

        int currentSum = 0;
        foreach (var enemy in _data.Enemies)
        {
            currentSum += enemy.ChancePoints;
            if (randomPoint < currentSum)
            {
                return enemy.EnemyPrefab;
            }
        }

        return null;
    }
}
