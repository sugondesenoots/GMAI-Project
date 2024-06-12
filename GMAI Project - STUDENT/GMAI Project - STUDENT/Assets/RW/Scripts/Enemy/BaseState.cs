using UnityEngine; 

public class BaseState : MonoBehaviour
{
    protected EnemyController controller;

    public BaseState(EnemyController controller)
    {
        this.controller = controller;
    }  

    public virtual void Enter(EnemyController controller)
    {

    }

    public virtual void Execute(EnemyController controller)
    {

    }

    public virtual void Exit(EnemyController controller)
    {

    }
}

//Framework reference: https://www.youtube.com/watch?v=Vt8aZDPzRjI