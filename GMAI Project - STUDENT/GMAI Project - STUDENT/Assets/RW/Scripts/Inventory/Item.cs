[System.Serializable]
public class Item
{
    public string itemName;
    public int itemCount;

    //Initializes the item with a name & count
    public Item(string name, int count)
    {
        itemName = name;
        itemCount = count;
    }

    //[System.Serializable] ensures the Item class is serializable
    //This is so it can be saved & loaded later on
}
