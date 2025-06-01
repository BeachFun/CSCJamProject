using Zenject;

public class VictoryScreen : ScreenBase
{
    [Inject] private GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();

        GameManagersInstaller.DiContainer.Inject(this);
    }

    public void Exit()
    {
        gameManager?.Exit();
    }
}