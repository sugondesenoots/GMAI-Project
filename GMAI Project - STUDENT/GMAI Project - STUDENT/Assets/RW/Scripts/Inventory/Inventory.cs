using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> items = new List<Item>(); 

    public GameObject foodPrefab;
    private GameObject foodInRange;

    public bool foodTaken = false;
    private bool isPlayerInRange = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPlayerInRange && !foodTaken)
                PickUpItem();
            else if (foodTaken)
                PlaceItem();
        }
    }

    public void AddItem(Item itemToAdd)
    {
        Item existingItem = items.Find(item => item.itemName == itemToAdd.itemName);
        if (existingItem != null)
            existingItem.itemCount += itemToAdd.itemCount;
        else
            items.Add(itemToAdd);

        Debug.Log(itemToAdd.itemCount + " " + itemToAdd.itemName + " Added to inventory");
    }

    public void RemoveItem(Item itemToRemove)
    {
        Item existingItem = items.Find(item => item.itemName == itemToRemove.itemName);
        if (existingItem != null)
        {
            existingItem.itemCount--;
            if (existingItem.itemCount <= 0)
            {
                items.Remove(existingItem);
                Debug.Log("Removed " + itemToRemove.itemName + " from inventory");
            }
            else
            {
                Debug.Log("Item count for " + itemToRemove.itemName + " decreased to " + existingItem.itemCount);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            isPlayerInRange = true;
            foodInRange = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            isPlayerInRange = false;
        }
    }

    private void PickUpItem()
    {
        if (!foodTaken && foodInRange != null)
        {
            Item itemToAdd = new Item("Food", 1); //Creates a new instance of Item with count 1, allows replay of placing and picking up object
            AddItem(itemToAdd);

            foodTaken = true;
            Destroy(foodInRange); 
            foodInRange = null; //Resets foodInRange reference
        }
    }

    private void PlaceItem()
    {
        Item itemToPlace = items.Find(item => item.itemName == "Food");
        if (itemToPlace != null && itemToPlace.itemCount == 1)
        {
            RemoveItem(itemToPlace);

            Vector3 playerPosition = transform.position;
            Vector3 playerForward = transform.forward;
            float distanceAhead = 1.0f;
            Vector3 spawnPosition = playerPosition + playerForward * distanceAhead;

            RaycastHit hit;
            if (Physics.Raycast(spawnPosition + Vector3.up * 2f, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                spawnPosition = hit.point;
            }

            Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Food is placed!");

            foodTaken = false;
        }
        else
        {
            Debug.Log("Item not found in inventory!");
        }
    }
}