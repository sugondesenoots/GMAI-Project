using UnityEngine;
using UnityEngine.AI;
using Panda;

public class CreatureStateManager : MonoBehaviour
{
    public PandaBehaviour behaviorTree; 
    public NavMeshAgent creatureAgent;
    public Animator animator; 
      
    //State scripts
    public CreatureIdle creatureIdle;
    public CreatureRoam creatureRoam;
    public CreatureRetreat creatureRetreat;  
    public CreatureFollowFood creatureFollowFood;
    public CreatureEat creatureEat; 

    public string currentStateName;

    private void Start()
    {
        SetCurrentState("CreatureIdle");
    }

    public void SetCurrentState(string stateName)
    {
        currentStateName = stateName;
        behaviorTree.enabled = true;
    }
}