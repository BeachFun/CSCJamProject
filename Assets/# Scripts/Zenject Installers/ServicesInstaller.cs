using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoadingService>().FromNew().AsSingle().NonLazy();
        Container.Bind<InputService>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SettingService>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UIService>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}