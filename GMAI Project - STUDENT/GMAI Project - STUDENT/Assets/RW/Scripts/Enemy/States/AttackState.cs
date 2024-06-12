using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    public EnemyController enemyController;

    public AttackState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override void Enter(EnemyController controller)
    {

    }

    public override void Execute(EnemyController controller)
    {

    }

    public override void Exit(EnemyController controller)
    {

    }
}
