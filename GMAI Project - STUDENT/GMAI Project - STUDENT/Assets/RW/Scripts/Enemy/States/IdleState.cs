using UnityEngine;

public class IdleState : BaseState
{ 
    //Needed scripts
    public EnemyController enemyController; 

    //Bools to control behaviours 
    public bool isResting;

    //Variables to control rest time 
    public float restTime = 3f;
    public float currentRestTime;

    public IdleState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override void Enter(EnemyController controller)
    {
        isResting = true;
        Debug.Log("Resting...");
    }

    public override void Execute(EnemyController controller)
    {
        if (isResting)
        { 
            //Updates current rest time
            currentRestTime += Time.deltaTime;

            //Checks if rest time is complete
            if (currentRestTime >= restTime)
            {
                isResting = false; 
                Exit(enemyController);

                Debug.Log("Rest time complete!");
                enemyController.SwitchState(enemyController.patrolState); 
            }
        }
    }

    public override void Exit(EnemyController controller)
    {
        currentRestTime = 0f; //Reset current rest time when exiting state
    }
}

//Framework reference: https://www.youtube.com/watch?v=Vt8aZDPzRjI