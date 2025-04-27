using UnityEngine;

/// <summary>
/// Скрипт спавнит переданный ему объект в зоне 2д коллайдера
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SpawnZone2D : MonoBehaviour
{
    // Кол-во попыток спавна объекта в рамках 2д зоны (круг, элипс, квадрад, прямоугольних и т.п.)
    private int maxAttempts = 100;

    private Collider2D _spawnArea;


    private void Awake()
    {
        _spawnArea = GetComponent<Collider2D>();
    }


    public GameObject Spawn(GameObject prefabToSpawn)
    {
        Bounds bounds = _spawnArea.bounds;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            if (_spawnArea.OverlapPoint(randomPoint))
            {
                return Instantiate(prefabToSpawn, randomPoint, Quaternion.identity);
            }
        }

        Debug.LogWarning("Не удалось найти подходящую точку для спавна");
        return null;
    }
}
