using UnityEngine;
public class PlayerState : BaseState
{
    public PlayerState(GameManager gameManager) : base(gameManager) { }

    public override void Enter()
    {
      //  InputController.instance.onMove += OnMove;
    }

    public override void Exit()
    {
       // InputController.instance.onMove -= OnMove;
    }

    public override void NextState()
    {
        
    }

    public override void Update()
    {

    }

    private void OnMove(Vector2 dir)
    {
        gameManager.player.rb.linearVelocity = new Vector3(dir.x * gameManager.player.movementSpeed, 0, dir.y * gameManager.player.movementSpeed);
    }
}
