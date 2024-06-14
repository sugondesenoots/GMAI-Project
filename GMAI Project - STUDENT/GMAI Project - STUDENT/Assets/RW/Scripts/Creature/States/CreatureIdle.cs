using Panda;
using UnityEngine;

public class CreatureIdle : MonoBehaviour
{
    public CreatureStateManager _stateManager;
    private bool isOthers;
    public float checkRadius = 3f;

    public void Initialize(CreatureStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    [Task]
    public bool IsIdleState()
    {
        return _stateManager.currentStateName == "CreatureIdle";
    }

    [Task]
    void CheckForOthersIdle()
    {
        isOthers = false; 

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("EnemyNPC") || hitCollider.CompareTag("Player"))
            {
                isOthers = true;
                _stateManager.animator.SetBool("isOthers", true);
                Debug.Log("Detected other beings, hiding...");
                break;
            } 
            else
            {
                isOthers = false;
                Debug.Log("No other beings, roaming...");
                Task.current.Succeed();
                break; 
            }
        }
    }

    [Task]
    void SwitchToRoam()
    {
        if (!isOthers)
        {
            _stateManager.SetCurrentState("CreatureRoam");
            Task.current.Succeed();

            Debug.Log("Switching to roaming");
        }
        else
        {
            Task.current.Fail();
        }
    }
}
