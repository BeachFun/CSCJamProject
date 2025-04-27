using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using RGames.Core;
using System.Linq;

public class EnemyManager : MonoBehaviour, IManager
{
    [Inject] private GameManager _gameManager;

    private ReactiveCollection<ZombieController> _enemyList = new();

    public ReactiveCollection<ZombieController> EnemyList => _enemyList;

    public ManagerStatus Status { get; private set; }


    private void Awake()
    {
        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler);
    }


    public void AddEnemy(GameObject enemy)
    {
        if (enemy is null) return;

        var controller = enemy.GetComponent<ZombieController>();

        if (controller is null) return;

        _enemyList.Add(controller);
        controller.OnDead += OnZombieDeadHandler;
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Played)
        {
            Status = ManagerStatus.Started;
            EnemyList.ToList().ForEach(x => x.WalkIsOn = true);
        }
        else
        {
            Status = ManagerStatus.Suspended;
            EnemyList.ToList().ForEach(x => x.WalkIsOn = false);
        }
    }

    private void OnZombieDeadHandler(ZombieController controller)
    {
        if (controller is not null) return;

        if (EnemyList.Contains(controller))
            EnemyList.Remove(controller);
    }
}
