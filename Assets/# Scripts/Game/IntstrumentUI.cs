using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class IntstrumentUI : MonoBehaviour
{
    [Header("Bindings")]
    [SerializeField] private Image logo;
    [SerializeField] private Image bar;
    [Header("References")]
    [SerializeField] private Instrument instrument;

    private void Awake()
    {
        instrument.StaminaPrecent.Subscribe(BarUpdate);
        instrument.IsAvailable.Subscribe(LogoUpdate);
    }

    private void BarUpdate(float value)
    {
        bar.fillAmount = value;
    }

    private void LogoUpdate(bool state)
    {
        logo.color = state
            ? Color.white
            : Color.gray;
    }
}