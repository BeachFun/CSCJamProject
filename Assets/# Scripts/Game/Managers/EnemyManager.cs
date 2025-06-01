using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;
using RGames.Core;

public class EnemyManager : ServiceBase
{
    [Header("Settings")]
    [SerializeField] private bool _zombiesIsImmutable;

    [Inject] private readonly GameManager _gameManager;

    private readonly ReactiveCollection<ZombieController> _enemyList = new();

    public ReactiveCollection<ZombieController> EnemyList => _enemyList;


    #region [Методы] Инициализация и запуск

    public override void Startup()
    {
        _gameManager.CurrentGameState
            .Subscribe(OnGameStateChangedHandler)
            .AddTo(this);
    }

    #endregion


    public void AddEnemy(GameObject enemy)
    {
        if (enemy == null) return;

        if (enemy.TryGetComponent<ZombieController>(out ZombieController controller))
        {
            controller.IsImmutalbe = _zombiesIsImmutable;
            _enemyList.Add(controller);
            controller.OnDead += OnZombieDeadHandler;
        }
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Played)
        {
            this.status.Value = ServiceStatus.Started;
            EnemyList.ToList().ForEach(x => x.WalkIsOn = true);
        }
        else
        {
            this.status.Value = ServiceStatus.Suspended;
            EnemyList.ToList().ForEach(x => x.WalkIsOn = false);
        }
    }

    private void OnZombieDeadHandler(ZombieController controller)
    {
        if (controller != null) return;

        if (EnemyList.Contains(controller))
            EnemyList.Remove(controller);
    }
}
