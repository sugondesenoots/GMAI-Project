using Panda;
using UnityEngine;

public class CreatureIdle : MonoBehaviour
{
    public CreatureStateManager _stateManager;
    private bool isOthers;

    public void Initialize(CreatureStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    [Task]
    public bool IsIdleState()
    {
        return _stateManager.currentStateName == "Idle"; 
    }

    [Task]
    void CheckForOthers()
    {
        isOthers = false; // Reset the state
        float checkRadius = 10f; // Define the radius to check for others
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("EnemyNPC") || hitCollider.CompareTag("Player"))
            {
                isOthers = true;
                _stateManager.animator.SetBool("isOthers", true);
                break;
            }
        }

        Task.current.Succeed();
    }

    [Task]
    void SwitchToRoam()
    {
        if (!isOthers)
        {
            _stateManager.SetCurrentState("Roam");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
