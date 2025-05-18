using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SimpleButton2D : MonoBehaviour
{
    [Header("References")]
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public Sprite pressedSprite;
    [Header("Actions"), Space]
    public UnityEvent onClick;

    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;


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
        isPressed = !isPressed;

        if (isPressed)
            spriteRenderer.sprite = pressedSprite;
        else
            spriteRenderer.sprite = highlightedSprite;

        onClick.Invoke();
    }
}
