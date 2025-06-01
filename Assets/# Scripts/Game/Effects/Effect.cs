using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Effect : MonoBehaviour
{
    protected abstract void Use(ZombieController zombie);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Use(collision.GetComponent<ZombieController>());
        }
    }
}
