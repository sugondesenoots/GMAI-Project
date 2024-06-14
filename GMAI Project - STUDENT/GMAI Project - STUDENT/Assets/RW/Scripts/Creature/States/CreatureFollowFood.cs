using Panda;
using UnityEngine;
using UnityEngine.AI;

public class CreatureFollowFood : MonoBehaviour
{
    public CreatureStateManager _stateManager;
    public Inventory inventory;

    public GameObject player;

    public float checkRadius = 10f; //Radius where creature follows food 

    public void Initialize(CreatureStateManager stateManager)
    {
        _stateManager = stateManager;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    [Task]
    public bool IsFollowFoodState()
    {
        return _stateManager.currentStateName == "CreatureFollowFood";
    }

    [Task]
    void FollowFood()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                if (inventory.playerHasFood)
                {
                    //Check distance between creature and player holding food
                    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position); 

                    if (distanceToPlayer <= checkRadius)
                    {
                        //Move towards the player
                        _stateManager.creatureAgent.SetDestination(player.transform.position);
                    }
                    Task.current.Succeed();
                }
                else
                {
                    Task.current.Fail();
                }
            }
        }
    }

    [Task]
    void SwitchToRoamOrEat()
    {
        if (!inventory.playerHasFood && !inventory.placedFood)
        {
            _stateManager.SetCurrentState("CreatureRoam");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        if (!inventory.playerHasFood && inventory.placedFood)
        {
            _stateManager.SetCurrentState("CreatureEat");
            Task.current.Succeed();
            Debug.Log("EatState");
            return;
        }
        else
        {
            Task.current.Fail();
        }
    }
}