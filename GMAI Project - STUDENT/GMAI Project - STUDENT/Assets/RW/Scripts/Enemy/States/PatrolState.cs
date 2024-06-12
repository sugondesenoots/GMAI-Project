using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    //Needed scripts
    public EnemyController enemyController; 

    //Bools to handle behaviours
    public bool seePlayer;

    //Variables for patrolling
    public float patrolRange; 
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
        //Updates time till rest
        timeTillRest -= Time.deltaTime;

        enemyController.animator.SetBool("isPatrol", true);

        if (enemyController.enemyNPC.remainingDistance <= enemyController.enemyNPC.stoppingDistance) //Checks if enemy npc is done pathing
        {
            Vector3 point; 

            if (RandomPoint(centrePoint.position, patrolRange, out point)) //Pass in our centre point and radius of area
            {
                //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //Visualizes it if you use gizmos
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
            enemyController.animator.SetBool("isPatrol", false);
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
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //Sets a random point in a sphere 
        NavMeshHit hit; 

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}

//Reference: https://www.youtube.com/watch?v=dYs0WRzzoRc&list=LL&index=1 
//Documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html 