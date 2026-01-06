public abstract class BaseState
{
    protected GameManager gameManager;

    public BaseState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void NextState();

}