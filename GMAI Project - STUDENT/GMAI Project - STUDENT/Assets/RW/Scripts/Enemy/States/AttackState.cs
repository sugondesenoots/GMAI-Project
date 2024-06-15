using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    public EnemyController enemyController; 
 
    public float attackRange = 1f; 
     
    //Player components
    public GameObject player;
    public LayerMask playerLayer; 

    public AttackState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override void Enter(EnemyController controller)
    {
        Debug.Log("Enter Attack State");
    }

    public override void Execute(EnemyController controller) 
    {
        enemyController.animator.SetBool("Seek", true);
        Vector3 directionToPlayer = player.transform.position - enemyController.transform.position;

        RaycastHit hit;      

        //Use raycast to check if the player is within attack range, also good when there are obstacles blocking player from enemy npc
        if (Physics.Raycast(enemyController.transform.position, directionToPlayer, out hit, attackRange, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                enemyController.animator.SetBool("Seek", false);
                enemyController.animator.SetBool("Attack", true);
                enemyController.enemyNPC.SetDestination(player.transform.position);
            }
            else
            {
                //Switches back to SeekState if the player is out of attack range or not visible
                enemyController.SwitchState(enemyController.seekState);
                Debug.Log("Switching to Seek State");

                Exit(controller);
            }
        }
        else
        { 
            //Same logic above
            enemyController.SwitchState(enemyController.seekState);
            Debug.Log("Switching to Seek State");

            Exit(controller);
        }

        //Continues seeking the player while attacking
        enemyController.enemyNPC.SetDestination(player.transform.position);
    }

    public override void Exit(EnemyController controller)
    {
        enemyController.animator.SetBool("Attack", false);
    }
}
