using UnityEngine;
using Zenject;

public class ToggleButton2DGroup : MonoBehaviour
{
    [Header("Settgins")]
    [SerializeField] private InstrumentEnum _instrument;

    [Inject] private InstrumentManager instrumentManager;

    private ToggleButton2D _currentToggled;


    public void OnButtonClicked(ToggleButton2D clickedButton)
    {
        // Деактивация
        if (_currentToggled == clickedButton)
        {
            clickedButton.SetPressed(false);
            _currentToggled = null;

            instrumentManager.SetInstrumentActive(_instrument, false);
        }
        // Активация
        else
        {
            if (_currentToggled != null)
                _currentToggled.SetPressed(false);

            clickedButton.SetPressed(true);
            _currentToggled = clickedButton;

            instrumentManager.SetInstrumentActive(_instrument, true);
        }
    }
}
