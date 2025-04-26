using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Binding")]
    [SerializeField] private SpawnZone2D[] _spawnZones;


    private void Awake()
    {
        print("Spawn Manager is initialized");
    }

    private void Start()
    {
        print("Spawn Manager is Started");
    }
}
