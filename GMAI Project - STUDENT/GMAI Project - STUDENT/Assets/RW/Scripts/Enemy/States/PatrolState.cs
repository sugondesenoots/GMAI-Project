using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{ 
    //Needed scripts
    private EnemyController enemyController; 
    public SeekState seekState;

    //Bools to handle behaviours
    public bool seePlayer;
    private bool isMoving;

    //Variables to control patrollings
    public float patrolRange = 10f;
    private Vector3 patrolPos;

    public PatrolState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override BaseState RunCurrentState()
    {
        if (seePlayer)
        {
            return seekState;
        } 
        else
        {
            return this;
        }
    }

    public override void Enter()
    {
        seePlayer = false; 
        SetPatrolPos();
    }

    public override void Execute()
    {
        if (!isMoving)
        {
            //Checks if it has reached the destination
            if (!enemyController.enemyNPC.pathPending && enemyController.enemyNPC.remainingDistance <= enemyController.enemyNPC.stoppingDistance)
            {
                isMoving = false;
                SetPatrolPos();
            }
        }
    }

    public override void Exit()
    {

    }

    private void SetPatrolPos()
    {
        //Sets a random position within the patrolling range
        Vector3 randomPosition = Random.insideUnitSphere * patrolRange + enemyController.transform.position;

        NavMeshHit hit;

        //Checks if the given position is on the NavMeshSurface
        if (NavMesh.SamplePosition(randomPosition, out hit, patrolRange, NavMesh.AllAreas))
        { 
            patrolPos = hit.position;
            enemyController.enemyNPC.SetDestination(patrolPos); 

            isMoving = true;
            enemyController.animator.SetTrigger("Patrol");
        }
    }
}
