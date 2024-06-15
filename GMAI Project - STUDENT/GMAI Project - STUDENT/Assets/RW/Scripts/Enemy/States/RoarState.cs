using UnityEngine;
using UnityEngine.AI; 

public class RoarState : BaseState
{
    public EnemyController enemyController;  
    public SeekState seekState;

    public float roarDuration = 2f; //Check animation length and input respective length
    private float roarTimer = 0f;

    public RoarState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override void Enter(EnemyController controller)
    { 
        Debug.Log("Enter Roar State");

        //Set the destination to the current position so npc stay stills while roaring
        enemyController.enemyNPC.SetDestination(enemyController.transform.position);
        enemyController.animator.SetBool("Roar", true);

        roarTimer = 0f;
    }

    public override void Execute(EnemyController controller)
    {
        roarTimer += Time.deltaTime;

        //Checks if the roar animation has finished
        if (roarTimer >= roarDuration)
        { 
            enemyController.SwitchState(enemyController.seekState);
            Exit(controller);
        }
    }

    public override void Exit(EnemyController controller)
    {
        enemyController.animator.SetBool("Roar", false);
    }
}
