using Zenject;

public class GameoverScreen : ScreenBase
{
    [Inject] private GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();

        GameManagersInstaller.DiContainer.Inject(this);
    }

    public void Restart()
    {
        gameManager?.Restart();
    }

    public void Exit()
    {
        gameManager?.Exit();
    }
}
