using UnityEngine;
using Zenject;
using UniRx;
using RGames.Core;
using Cysharp.Threading.Tasks;

public class SpawnManager : MonoBehaviour, IManager
{
    [Header("Settings - main")]
    [SerializeField] private float _intervalSpawn;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _intervalAcceleration;
    [SerializeField, Range(0.0f, 1.0f)] private float _stepAccelerationTime;
    [SerializeField] private float _minSpawnInternval;

    [Header("Settings - spawn prefabs")]
    [SerializeField] private SpawnManagerData _data;

    [Header("Binding")]
    [SerializeField] private SpawnZone2D[] _spawnZones;


    [Inject] private GameManager _gameManager;
    [Inject] private EnemyManager _enemyManager;

    private bool m_accelerationIsOn = true;
    private float m_lastElapsedTime;
    private float m_timerSpawn;
    private float m_timerAcceleration;
    private float m_intervalSpawn;

    public ManagerStatus Status { get; private set; }


    private void Awake()
    {
        Status = ManagerStatus.Initializing;

        _data = (SpawnManagerData)_data.Clone();

        SetIntervalSpawn();

        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler);
        _gameManager.ElapsedTime.Subscribe(OnTimeUpdateHandler);

        print("Spawn Manager is initialized");
    }

    private void Start()
    {
        Status = ManagerStatus.Started;

        print("Spawn Manager is Started");
    }


    [ContextMenu("Тест - Zombie Spawn")]
    public void Spawn()
    {
        GameObject enemyPrefab = GetRandomEnemy();

        if (enemyPrefab is null) return;

        _enemyManager.AddEnemy(_spawnZones[Random.Range(0, _spawnZones.Length)].Spawn(enemyPrefab));

        m_timerSpawn = 0;
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


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Played)
        {
            Status = ManagerStatus.Started;
        }
        else
        {
            Status = ManagerStatus.Suspended;
        }
    }


    private void OnTimeUpdateHandler(float elapsedTime)
    {
        if (Status != ManagerStatus.Started) return;

        float interval = (elapsedTime - m_lastElapsedTime);

        if (m_accelerationIsOn) AccelerationCalc(ref elapsedTime, ref interval);
        SpawnCalc(elapsedTime, interval);

        m_lastElapsedTime = elapsedTime;
    }

    /// <summary>
    /// Вычисление времени спавна
    /// </summary>
    /// <param name="elapsedTime"></param>
    private async void SpawnCalc(float elapsedTime, float interval)
    {
        m_timerSpawn += interval;

        // Спавн, если время спавна пришло
        if (m_timerSpawn >= m_intervalSpawn)
        {
            m_timerSpawn = 0;

            SetIntervalSpawn();
            await UniTask.WaitWhile(() => Status != ManagerStatus.Started);

            Spawn();
        }
    }

    /// <summary>
    /// Вычисление ускорения спавна
    /// </summary>
    private void AccelerationCalc(ref float elapsedTime, ref float interval)
    {
        m_timerAcceleration += interval;

        // Ускорение спавна, если время шага пришло
        if (m_timerAcceleration >= _intervalAcceleration)
        {
            float minusTime = Mathf.Min(_stepAccelerationTime, _intervalSpawn - _minSpawnInternval);

            _intervalSpawn -= minusTime;

            // Отключение ускорения, если интервал дошел до минимальной границы
            if (minusTime < _stepAccelerationTime)
            {
                m_accelerationIsOn = false;
            }

            m_timerAcceleration = 0;
        }
    }

    private void SetIntervalSpawn()
    {
        m_intervalSpawn = _intervalSpawn + Random.Range(0, (int)_spawnDelay * 1000);
    }
}
