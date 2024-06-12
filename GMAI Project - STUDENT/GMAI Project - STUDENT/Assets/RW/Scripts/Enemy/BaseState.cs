using UnityEngine; 

public abstract class BaseState
{
    protected EnemyController controller;

    public BaseState(EnemyController controller)
    {
        this.controller = controller;
    }  
     
    public abstract BaseState RunCurrentState();    

    public virtual void Enter()
    {

    }

    public virtual void Execute()
    {

    }

    public virtual void Exit()
    {

    }
}

//Framework reference: https://www.youtube.com/watch?v=cnpJtheBLLY