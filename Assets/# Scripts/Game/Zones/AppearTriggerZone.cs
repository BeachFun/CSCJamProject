using UnityEngine;

public class AppearTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ZombieController>().YouAreAppear();
        }
    }
}
