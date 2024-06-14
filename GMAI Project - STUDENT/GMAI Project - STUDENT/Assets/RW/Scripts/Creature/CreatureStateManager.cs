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

    public string currentStateName;

    private void Start()
    {
        SetCurrentState("CreatureIdle");

        ////Initialize all states
        //creatureIdle.Initialize(this);
        //creatureRoam.Initialize(this);
        //creatureRetreat.Initialize(this);
    }

    private void Update()
    {

    }

    public void SetCurrentState(string stateName)
    {
        currentStateName = stateName;
        behaviorTree.enabled = true;
    }
}
