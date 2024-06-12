using UnityEngine;
using UnityEngine.AI;

public class SeekState : BaseState
{
    public EnemyController enemyController;

    public SeekState(EnemyController controller) : base(controller)
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
