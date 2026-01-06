using UnityEngine;

public class LoseState : BaseState
{
    public LoseState(GameManager gameManager) : base(gameManager) { }

    public override void Enter()
    {
        gameManager.losePanel.SetActive(true);
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
