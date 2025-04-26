using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class HUDCanvasConttollerUI : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    public void Pause()
    {
        _gameManager?.ChangeGameState(GameState.Paused);
    }
}