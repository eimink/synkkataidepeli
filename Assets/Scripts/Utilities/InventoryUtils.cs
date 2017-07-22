using System.Collections.Generic;

public class InventoryUtils
{
    private int collectablesCount;
    private List<string> inventory = new List<string>();

    public void setInventoryData(string item)
    {
        inventory.Add(item);
    }
    public List<string> getInventoryData()
    {
        return inventory;
    }

    public void collectItem(int collected)
    {
        collectablesCount += collected;
    }
    public int collectedItems()
    {
        return collectablesCount;
    }
}