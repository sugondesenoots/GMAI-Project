using Panda;
using UnityEngine;
using UnityEngine.AI;

public class CreatureRoam : MonoBehaviour
{
    public CreatureStateManager _stateManager;
    public Inventory inventory;

    //Roaming variables
    public NavMeshAgent creatureAgent;
    public float roamRange;
    public Transform centrePoint;
    private Vector3 roamDestination; 
     
    //Bools for state transitions
    private bool isOthers;
    public float checkRadius = 3f;

    public void Initialize(CreatureStateManager stateManager, NavMeshAgent agent, Transform centre, float range)
    {
        _stateManager = stateManager;
        creatureAgent = agent;
        centrePoint = centre;
        roamRange = range;
    }

    [Task]
    public bool IsRoamState()
    {
        return _stateManager.currentStateName == "CreatureRoam";
    }

    [Task]
    void Roam()
    {
        if (creatureAgent.remainingDistance <= creatureAgent.stoppingDistance)
        {
            if (RandomPoint(centrePoint.position, roamRange, out roamDestination))
            {
                Debug.DrawRay(roamDestination, Vector3.up, Color.blue, 1.0f);
                creatureAgent.SetDestination(roamDestination);
            }
        } 

        Task.current.Succeed();
    }

    [Task]
    void CheckForOthersRoam()
    {
        isOthers = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("EnemyNPC") || hitCollider.CompareTag("Player"))
            {
                isOthers = true;
                break;
            } 
            else
            {
                isOthers = false;
            }
        }

        Task.current.Succeed();
    }

    [Task]
    void SwitchToRetreatOrFollow()
    {
        if (isOthers)
        {
            _stateManager.SetCurrentState("CreatureRetreat");
            Task.current.Succeed();
        }
        if (inventory.playerHasFood && isOthers)
        {
            _stateManager.SetCurrentState("CreatureFollowFood");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
     
    //Random generated position for roaming (Used this for my EnemyNPC FSM)
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

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
