using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{ 
    //Base state script
    public BaseState currentState;

    //State scripts 
    public IdleState idleState; 
    public PatrolState patrolState; 
    public SeekState seekState;  
    public RoarState roarState;
    public AttackState attackState; 
     
    //Animatior components
    public Animator animator;

    //NavMesh components 
    public NavMeshAgent enemyNPC; 
     
    //Navigation components
    public LayerMask ground, player;

    void Start()
    {
        currentState = idleState;
        currentState.Enter(this);
    }

    void Update()
    {
        currentState.Execute(this);
    }

    public void SwitchState(BaseState state)
    {
        currentState = state;
        state.Enter(this);
    }
}

//Framework: https://www.youtube.com/watch?v=Vt8aZDPzRjI 