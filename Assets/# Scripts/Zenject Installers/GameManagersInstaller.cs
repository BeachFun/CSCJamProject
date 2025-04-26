using Zenject;

public class GameManagersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
