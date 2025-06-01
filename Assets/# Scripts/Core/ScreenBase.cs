using UnityEngine;

public class ScreenBase : MonoBehaviour, IScreen<ScreenBase>
{
    [Tooltip("Будет ли экран виден после инициализации")]
    [SerializeField] private bool isVisible;

    public bool IsVisible => isVisible;


    protected virtual void Awake()
    {
        Show(isVisible);
    }

    public void Show(bool isShow)
    {
        isVisible = isShow;
        this.gameObject.SetActive(isShow);
    }
}
