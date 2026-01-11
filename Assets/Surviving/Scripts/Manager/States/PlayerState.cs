using UnityEngine;
public class PlayerState : BaseState
{
    public PlayerState(GameManager gameManager) : base(gameManager) { }

    public override void Enter()
    {
        GesturesReader.instance.onDrag += OnMove;
        GesturesReader.instance.onDragEnd += OnMoveCancelled;
    }

    public override void Exit()
    {
        GesturesReader.instance.onDrag -= OnMove;
        GesturesReader.instance.onDragEnd -= OnMoveCancelled;
    }

    public override void NextState()
    {

    }

    public override void Update()
    {

    }

    private void OnMove(Vector2 dir, Vector2 dir1)
    {
        Vector3 move = new Vector3(dir.x, 0f, dir.y).normalized;

        var rb = gameManager.player.rb;
        float speed = gameManager.player.movementSpeed;

        rb.linearVelocity = new Vector3(move.x * speed, rb.linearVelocity.y, move.z * speed);
    }

   
    private void OnMoveCancelled(Vector2 dir)
    {
        var rb = gameManager.player.rb;
        rb.linearVelocity = Vector3.zero;
    }
}
