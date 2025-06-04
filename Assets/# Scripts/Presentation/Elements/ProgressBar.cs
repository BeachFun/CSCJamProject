using DG.Tweening;
using FriendNote.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour, IDataUpdatable<float>, IRefreshable
{
    [Header("Settings")]
    [Range(0f, .5f)]
    [SerializeField] private float animationSmoothDuration = 0.1f;
    [Header("Bindings")]
    [SerializeField] private TMP_Text textBar;
    [SerializeField] private Image imageProggres;

    private float progress;

    public void Refresh()
    {
        textBar?.SetText(progress.ToString("P0"));
        imageProggres?.DOFillAmount(progress, animationSmoothDuration);
    }

    public void UpdateData(float data)
    {
        progress = data;
        Refresh();
    }
}