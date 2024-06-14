using Panda;
using UnityEngine;

public class CreatureEat : MonoBehaviour
{
    public CreatureStateManager _stateManager;

    public bool foodEaten = false;
     
    //Eating duration
    private float eatTimePassed = 0f;
    private float eatDuration = 5f; 

    public void Initialize(CreatureStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    [Task]
    public bool IsEatState()
    {
        return _stateManager.currentStateName == "CreatureEat";
    }

    [Task] 
    void Eating()
    {
        GameObject foodObject = Inventory.instance.GetFoodInRange();
        eatTimePassed += Time.deltaTime;

        if (foodObject != null)
        {
            _stateManager.animator.SetBool("Eating", true);
        }

        //Check if eating duration has passed
        if (eatTimePassed >= eatDuration)
        { 
            foodEaten = true; 
            Destroy(foodObject);
            Task.current.Succeed();
        }
    }

    [Task]
    void SwitchToRoamEat()
    {
        if (foodEaten)
        {
            _stateManager.animator.SetBool("Eating", false);
            _stateManager.SetCurrentState("CreatureRoam");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}