using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieLineDetector : MonoBehaviour
{
    public int LineNumber;
    public List<ZombieController> zombiesInRange = new List<ZombieController>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        ZombieController zombie = other.GetComponent<ZombieController>();
        if (zombie != null)
        {
            zombiesInRange.Add(zombie);
        }

        zombiesInRange = zombiesInRange.Where(x => x != null).ToList();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ZombieController zombie = other.GetComponent<ZombieController>();
        if (zombie != null)
        {
            zombiesInRange.Remove(zombie);
        }
    }

    public ZombieController GetRandomZombie()
    {
        if (zombiesInRange.Count == 0)
            return null;

        zombiesInRange = zombiesInRange.Where(x => x != null).ToList();

        if (zombiesInRange.Count == 0)
            return null;

        int index = Random.Range(0, zombiesInRange.Count);
        return zombiesInRange[index];
    }
}
