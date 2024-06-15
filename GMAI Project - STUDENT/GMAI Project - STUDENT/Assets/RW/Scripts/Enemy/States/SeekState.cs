using UnityEngine;
using UnityEngine.AI;

public class SeekState : BaseState
{
    public EnemyController enemyController;

    //Bools to handle state transitions
    private bool inAttackRange;
    private bool seePlayer; 

    public float seekRange = 10f;
    public float attackRange = 1f;

    //Player component 
    public GameObject player;

    //Steering parameters of agent
    private float originalSpeed;
    private float originalAcceleration;
    private float originalAngularSpeed;

    public SeekState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override void Enter(EnemyController controller)
    {
        seePlayer = true; 
        Debug.Log("Seeking player...");
         
        enemyController.animator.SetBool("Seek", true);
    }

    public override void Execute(EnemyController controller)
    {  
        //Updates and holds player position
        Vector3 playerPos = player.transform.position;
     
        //Sets the chase/seek animation + Updates player position when enemy npc is seeking player
        enemyController.enemyNPC.SetDestination(playerPos);
        enemyController.animator.SetBool("Seek", true);

        //Stores the original parameters for exit function
        originalSpeed = enemyController.enemyNPC.speed;
        originalAcceleration = enemyController.enemyNPC.acceleration;
        originalAngularSpeed = enemyController.enemyNPC.angularSpeed;

        //Increases steering parameters when chasing player
        enemyController.enemyNPC.speed = 3f;
        enemyController.enemyNPC.acceleration = 2f;
        enemyController.enemyNPC.angularSpeed = 450f;

        CheckPlayerStatus(); 

        if (inAttackRange) //Switches to AttackState if in attack range
        {
            enemyController.SwitchState(enemyController.attackState); 
            Debug.Log("Attack State");

            Exit(controller);
        }

        if (!seePlayer ) //Switches back to PatrolState if the player is no longer seen
        { 
            enemyController.SwitchState(enemyController.patrolState);
            Debug.Log("Patrol State");

            Exit(controller);
        } 
    }

    public override void Exit(EnemyController controller)
    {
        //Resets the animation + NavMeshAgent variables
        enemyController.animator.SetBool("Seek", false);
        enemyController.enemyNPC.speed = originalSpeed;
        enemyController.enemyNPC.acceleration = originalAcceleration;
        enemyController.enemyNPC.angularSpeed = originalAngularSpeed;
    }

    private void CheckPlayerStatus()
    {
        float distanceToPlayer = Vector3.Distance(enemyController.transform.position, player.transform.position);

        if (distanceToPlayer <= seekRange)
        {
            seePlayer = true;

            //Checks if player is within attack range
            if (distanceToPlayer <= attackRange)
            {
                inAttackRange = true; 
            }
            else
            {
                inAttackRange = false;
            }
        }
        else
        {
            seePlayer = false;
        }
    }
}
