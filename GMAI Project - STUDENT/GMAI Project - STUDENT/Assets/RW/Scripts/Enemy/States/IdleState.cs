using UnityEngine;

public class IdleState : BaseState
{ 
    //Needed scripts
    private EnemyController enemyController; 
    public PatrolState patrolState;

    //Bools to control behaviours 
    public bool isResting;

    //Variables to control rest time 
    public float restTime = 3f;
    public float currentRestTime;

    public IdleState(EnemyController controller) : base(controller)
    {
        enemyController = controller;
    }

    public override BaseState RunCurrentState()
    {
        if (!isResting)
        {
            return patrolState;
        }
        else
        {
            return this;
        }
    }

    public override void Enter()
    {
        isResting = true;
        enemyController.animator.SetTrigger("Idle");

        Debug.Log("Resting...");
    }

    public override void Execute()
    {
        if (isResting)
        { 
            //Updates current rest time
            currentRestTime += Time.deltaTime;

            //Checks if rest time is complete
            if (currentRestTime >= restTime)
            {
                isResting = false; 
                Exit();

                Debug.Log("Rest time complete!");
            }
        }
    }

    public override void Exit()
    {
        currentRestTime = 0f; //Reset current rest time when exiting state
    }
}

//Framework reference: https://www.youtube.com/watch?v=cnpJtheBLLY