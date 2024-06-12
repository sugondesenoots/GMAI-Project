using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    private EnemyController enemyController;

    public AttackState(EnemyController controller) : base(controller)
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
