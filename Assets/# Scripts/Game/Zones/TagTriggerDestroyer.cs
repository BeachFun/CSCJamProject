using UnityEngine;

public class TagTriggerDestroyer : MonoBehaviour
{
    [SerializeField] private string _tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag))
        {
            Destroy(collision.gameObject);
        }
    }
}