class Inventory
{
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;

    // constructor
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }
    // methods
    public int TotalWeight()
    {
        int total = 0;
        foreach (string key in items.Keys)
        {
            total += items[key].Weight;
        }
        return total;
    }
    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }
    public bool Put(string itemName, Item item)
    {
        if (FreeWeight() <= item.Weight)
        {
            items.Add(itemName, item);
            return true;
        }
        else
        {
            return false;
        }
    }
    public Item Get(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];
            items.Remove(itemName);
            return item;
        }
        else
        {
            return null;
        }
    }
    public string Show()
    {
        Console.WriteLine("you have " + FreeWeight() + "kg (kilogram for less smart people) left in your backpack.");
        if (items.Count == 0)
        {
            return "There are no items in your Inventory.";
        }
        else
        {
            return string.Join(", ", items.Keys);
        }
    }
}