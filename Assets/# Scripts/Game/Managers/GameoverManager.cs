using UnityEngine;
using UniRx;
using Zenject;
using RGames.Core;

public class GameoverManager : ServiceBase
{
    [SerializeField] private float _winTimeInSeconds = 300;

    [Inject] private TimeManager timeManager;
    [Inject] private PlayerManager playerManager;
    [Inject] private UIService uiService;

    private readonly ReactiveProperty<GameoverState> state = new();

    public ReadOnlyReactiveProperty<GameoverState> State => state.ToReadOnlyReactiveProperty();


    #region [Методы] Инициализация и запуск

    public override void Startup()
    {
        timeManager.ElapsedTime
            .Where(x => x >= _winTimeInSeconds)
            .Take(1)
            .Subscribe((x) => Finish(true))
            .AddTo(this);

        playerManager.Health
            .Where(x => x <= 0)
            .Take(1)
            .Subscribe((x) => Finish(false))
            .AddTo(this);
    }

    #endregion


    public void Finish(bool isWin)
    {
        state.Value = isWin
            ? GameoverState.Win
            : GameoverState.Lose;

        if (isWin)
            uiService?.OpenCanvas("VictoryScreen");
        else
            uiService?.OpenCanvas("GameoverScreen");
    }
}

public enum GameoverState
{
    None,
    Win,
    Lose
}