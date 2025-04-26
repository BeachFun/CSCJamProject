using UnityEngine;
using UnityEngine.Events;

public class EnemyTriggerZone : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ZombieController>().Kill();
            OnTriggerEnter?.Invoke();
        }
    }
}