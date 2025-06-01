using UnityEngine;
using Zenject;

public class ToggleButton2DGroup : MonoBehaviour
{
    [Header("Settgins")]
    [SerializeField] private ButtonMode _mode;
    [SerializeField] private Instrument _instrument;

    private ToggleButton2D _currentToggled;
    [Inject] InstrumentManager _manager;

    public void OnButtonClicked(ToggleButton2D clickedButton, int roadIndex)
    {
        if (_mode == ButtonMode.Toggle)
        {
            OnToggleHandler(clickedButton);
        }
        else
        {
            OnClickedHandler(clickedButton);
        }

        _manager.ChangeRoad(_instrument, roadIndex);
    }

    private void OnClickedHandler(ToggleButton2D clickedButton)
    {
        if (_currentToggled != null)
            _currentToggled.SetPressed(false);

        //clickedButton.SetPressed(true);
        //_currentToggled = clickedButton;
    }

    private void OnToggleHandler(ToggleButton2D clickedButton)
    {
        // Деактивация
        if (_currentToggled == clickedButton)
        {
            clickedButton.SetPressed(false);
            _currentToggled = null;
        }
        // Активация
        else
        {
            if (_currentToggled != null)
                _currentToggled.SetPressed(false);

            clickedButton.SetPressed(true);
            _currentToggled = clickedButton;
        }
    }


    public enum ButtonMode
    {
        Toggle,
        Ckick
    }
}
