using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Image[] hearts;

    public void UpdateHearts(int health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;
                hearts[i].color = Color.white;
            }
            else if (i == health)
            {
                hearts[i].enabled = true;
                hearts[i].color = new Color(1f, 1f, 1f, 0.5f);
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}