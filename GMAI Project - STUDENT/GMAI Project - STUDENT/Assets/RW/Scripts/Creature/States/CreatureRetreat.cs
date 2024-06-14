using Panda;
using UnityEngine;
using UnityEngine.AI;

public class CreatureRetreat : MonoBehaviour
{
    public CreatureStateManager _stateManager;
    public NavMeshAgent creatureAgent;
    public Transform bush; 
    private bool reachBush;

    public void Initialize(CreatureStateManager stateManager, NavMeshAgent agent, Transform bushTransform)
    {
        _stateManager = stateManager;
        creatureAgent = agent;
        bush = bushTransform;
    }

    [Task]
    public bool IsRetreatState()
    {
        return _stateManager.currentStateName == "CreatureRetreat";
    }

    [Task]
    void Retreat()
    {
        reachBush = false; 

        //Retreats to bush
        creatureAgent.SetDestination(bush.position);

        if (creatureAgent.remainingDistance <= creatureAgent.stoppingDistance)
        {
            //Reached bush
            reachBush = true;
            Debug.Log("Reached bush"); 

            Task.current.Succeed();
        } 
        else
        { 
            Task.current.Fail();
        }
    }

    [Task] 
    void SwitchToIdle()
    {
        if (reachBush)
        {
            _stateManager.SetCurrentState("CreatureIdle");
            Debug.Log("Switching to Idle");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
