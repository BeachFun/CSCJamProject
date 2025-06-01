using System;
using UnityEngine;

public class CanvasBase : MonoBehaviour, IScreen<CanvasBase>
{
    public bool IsVisible { get; private set; } = true;

    public event Action<CanvasBase, bool> OnVisibleUpdated;


    public void Show(bool isShow)
    {
        IsVisible = isShow;
        this.gameObject.SetActive(isShow);

        OnVisibleUpdated?.Invoke(this, isShow);
    }
}