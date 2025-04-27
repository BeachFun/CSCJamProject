using UnityEngine;
using UnityEngine.UI;

public class UIHealthManager : MonoBehaviour
{
    public Image[] hearts;  
    public PlayerManager playerManager;  

    private int previousHealth;

    private void Start()
    {
        previousHealth = playerManager.health;
        UpdateHearts();
    }

    private void Update()
    {
        if (playerManager.health != previousHealth)
        {
            UpdateHearts();
            previousHealth = playerManager.health;
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerManager.health)
            {
                hearts[i].color = Color.white; 
            }
            else if (i == playerManager.health)
            {
                Color fadedColor = Color.white;
                fadedColor.a = 0.5f; 
                hearts[i].color = fadedColor;
            }
            else
            {
                Color invisibleColor = Color.white;
                invisibleColor.a = 0f; 
                hearts[i].color = invisibleColor;
            }
        }

        if (playerManager.health <= 1)
        {
            Debug.Log("Game Over!");
        }
    }
}