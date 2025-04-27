using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UIHealthManager : MonoBehaviour
{
    public Image[] hearts;
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager.Health.Subscribe(UpdateHearts).AddTo(this);
        UpdateHearts(playerManager.Health.Value); // Сразу обновить при старте
    }

    private void UpdateHearts(int health)
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

        if (health <= 1)
        {
            Debug.Log("Game Over!");
            // Здесь можешь вызвать проигрыш
        }
    }
}