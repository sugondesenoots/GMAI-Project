using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    BaseState currentState; 
     
    //Animation components
    public Animator animator; 
     
    //NavMesh components 
    public NavMeshAgent enemyNPC;

    void Start()
    {
        currentState = new IdleState(this);

        Debug.Log("Starting idle state...");
    }

    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        BaseState nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(BaseState nextState)
    {
        currentState = nextState;
    }
}

//Framework reference: https://www.youtube.com/watch?v=cnpJtheBLLY