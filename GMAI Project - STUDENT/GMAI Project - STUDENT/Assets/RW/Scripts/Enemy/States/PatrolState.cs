using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    //Needed scripts
    public EnemyController enemyController;

    //Bools to handle behaviours
    public bool seePlayer;

    //Variables for patrolling
    public float range;
    public Transform centrePoint;

    //Variables to control rest 
    public float timeTillRest = 10f;

    public PatrolState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override void Enter(EnemyController controller)
    {
        seePlayer = false;
        Debug.Log("Patrolling surroundings...");
    }

    public override void Execute(EnemyController controller)
    {
        DetectPlayer();

        enemyController.animator.SetBool("Patrol", true);

        //Updates time till rest
        timeTillRest -= Time.deltaTime;

        if (enemyController.enemyNPC.remainingDistance <= enemyController.enemyNPC.stoppingDistance) //Completed pathing
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //Passes in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //Allows for visuals with/using gizmos
                enemyController.enemyNPC.SetDestination(point);
            }
        } 

        else if (seePlayer)
        {
            enemyController.SwitchState(enemyController.seekState);
            Debug.Log("Chasing player!");

            Exit(controller);
        } 

        else if (timeTillRest <= 0f)
        {
            enemyController.SwitchState(enemyController.idleState);
            enemyController.animator.SetBool("Patrol", false);

            Debug.Log("Going to rest...");
            enemyController.enemyNPC.SetDestination(enemyController.enemyNPC.transform.position);

            Exit(controller);
        }
    } 

    public override void Exit(EnemyController controller)
    {
        timeTillRest = 10f; //Reset time till rest
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //Random point in a sphere  

        NavMeshHit hit; 

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void DetectPlayer()
    {
        RaycastHit hit;

        //Raycasting from enemy npc transform.forward
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            //Checks if the raycast hits Player object
            if (hit.collider.CompareTag("Player"))
            {
                seePlayer = true;
            }
        }
    }
}

//Reference: https://www.youtube.com/watch?v=dYs0WRzzoRc&list=LL&index=1 
//Documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html 