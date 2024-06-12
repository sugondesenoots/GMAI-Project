using UnityEngine;
using UnityEngine.AI;

public class SeekState : BaseState
{
    private EnemyController enemyController;

    public SeekState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override BaseState RunCurrentState()
    {
        return this;
    }

    public override void Enter()
    {

    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }
}
