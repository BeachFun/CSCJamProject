using UnityEngine;

public class Sound : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }
}
