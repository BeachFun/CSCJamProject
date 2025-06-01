using Zenject;

public class GameManagersInstaller : MonoInstaller
{
    public static DiContainer DiContainer;

    public override void InstallBindings()
    {
        DiContainer = Container;

        Container.Bind<PlayerManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<EnemyManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SpawnManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<InstrumentManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<MusicManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<GameoverManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<TimeManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
