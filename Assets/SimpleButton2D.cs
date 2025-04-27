using UnityEngine;
using UnityEngine.Events; // Чтобы можно было назначить действия в инспекторе

public class SimpleButton2D : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public Sprite pressedSprite;

    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;

    public UnityEvent onClick; // Событие для клика

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;
    }

    private void OnMouseEnter()
    {
        if (!isPressed)
            spriteRenderer.sprite = highlightedSprite;
    }

    private void OnMouseExit()
    {
        if (!isPressed)
            spriteRenderer.sprite = normalSprite;
    }

    private void OnMouseDown()
    {
        spriteRenderer.sprite = pressedSprite;
        isPressed = true;
        onClick.Invoke(); // Выполняем действие
    }

    private void OnMouseUp()
    {
        isPressed = false;
        spriteRenderer.sprite = highlightedSprite; // Вернуться в состояние наведения
    }
}
