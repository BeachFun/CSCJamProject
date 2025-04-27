using UnityEngine;

public class ToolButtonController : MonoBehaviour
{
    [SerializeField] private ZombieLineDetector lineDetector; // Ссылка на ловушку этой линии

    public void OnToolButtonPressed()
    {
        if (lineDetector == null)
        {
            Debug.LogWarning("LineDetector не привязан к кнопке!");
            return;
        }

        ZombieController randomZombie = lineDetector.GetRandomZombie();
        if (randomZombie != null)
        {
            Destroy(randomZombie.gameObject); // Просто удаляем зомби
        }
        else
        {
            Debug.Log("На линии нет зомби!");
        }
    }
}
