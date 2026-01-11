
using UnityEngine;

public class WinState : BaseState
{
    public WinState(GameManager gameManager) : base(gameManager) { }
    public override void Enter()
    {
        gameManager.winPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public override void Exit()
    {
        
    }

    public override void NextState()
    {
        
    }

    public override void Update()
    {
        
    }
}
