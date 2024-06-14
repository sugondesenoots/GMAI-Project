using UnityEngine;
using UnityEngine.AI;
using Panda;

public class CreatureStateManager : MonoBehaviour
{
    public PandaBehaviour behaviorTree; 
    public NavMeshAgent creatureAgent;
    public Animator animator;

    public string currentStateName;
    private bool insideTriggerZone = false;

    private void Start()
    {
        SetCurrentState("CreatureIdle");
    }

    private void Update()
    {
        HandleTriggerZone();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideTriggerZone = false;
        }
    }

    private void HandleTriggerZone()
    {
        if (insideTriggerZone)
        {
            if (currentStateName == "CreatureIdle")
            {
                // Perform actions related to Idle state if needed
            }
        }
        else
        {
            // Perform actions when player exits trigger zone
        }
    }

    public void SetCurrentState(string stateName)
    {
        currentStateName = stateName;
        behaviorTree.enabled = true;
    }
}
