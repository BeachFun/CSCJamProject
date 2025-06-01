using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ToggleButton2D : MonoBehaviour
{
    [Header("Settgins")]
    [SerializeField] private int _road;

    [Header("References")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite highlightedSprite;
    [SerializeField] private Sprite pressedSprite;

    [Header("Group")]
    [SerializeField] private ToggleButton2DGroup group;

    private SpriteRenderer spriteRenderer;

    public bool IsPressed { get; private set; }


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;
    }

    private void OnMouseEnter()
    {
        if (!IsPressed)
            spriteRenderer.sprite = highlightedSprite;
    }

    private void OnMouseExit()
    {
        if (!IsPressed)
            spriteRenderer.sprite = normalSprite;
    }

    private void OnMouseDown()
    {
        group.OnButtonClicked(this, _road);
    }

    public void SetPressed(bool pressed)
    {
        IsPressed = pressed;

        if (IsPressed)
            spriteRenderer.sprite = pressedSprite;
        else
            spriteRenderer.sprite = normalSprite;
    }
}
