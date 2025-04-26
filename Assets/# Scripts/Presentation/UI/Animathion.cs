using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class NewMonoBehaviourScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [SerializeField] private Vector3 _hoverScale = new Vector3(1.1f, 1.1f, 1.0f);
    [SerializeField] private float _animationTime = .4f;

    private Vector3 _originalScale;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_hoverScale, _animationTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_originalScale, _animationTime / 2);
    }
}