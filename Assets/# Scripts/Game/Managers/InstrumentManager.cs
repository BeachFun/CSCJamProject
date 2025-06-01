using UnityEngine;
using UniRx;
using Zenject;
using RGames.Core;

public class InstrumentManager : ServiceBase
{
    [Header("Binding")]
    [SerializeField] private Transform[] _soundSpawnPoints;

    [Inject] private GameManager _gameManager;


    private readonly Instrument[] m_roads = new Instrument[3];


    #region [Методы] Инициализация и запуск

    public override void Startup()
    {
        _gameManager.CurrentGameState
            .Subscribe(OnGameStateChangedHandler)
            .AddTo(this);

        this.status.Value = ServiceStatus.Started;
    }

    #endregion


    public void ChangeRoad(Instrument value, int roadIndex)
    {
        value.transform.position = _soundSpawnPoints[roadIndex].position;

        // Если на дорожке такой же инструмент
        if (m_roads[roadIndex] == value)
        {
            if (value.Mode == InstrumentMode.Auto)
            {
                value.Stop();
                m_roads[roadIndex] = null;
            }
            else
            {
                value.Play();
            }
        }
        else
        {
            if (m_roads[roadIndex] != null)
                m_roads[roadIndex].Stop();
            m_roads[roadIndex] = value;
            value.Play();
        }
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        this.status.Value = state == GameState.Played
           ? ServiceStatus.Started
           : ServiceStatus.Suspended;
    }
}
