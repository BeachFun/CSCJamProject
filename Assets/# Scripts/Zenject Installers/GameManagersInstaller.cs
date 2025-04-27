using Zenject;

public class GameManagersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<EnemyManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SpawnManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<InstrumentManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
