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
    public bool Put(string itemName, Item item)
    {
        // TODO implementeer:
        // Check het gewicht van het Item
        if (maxWeight <= item.Weight)
        {
            Console.WriteLine("The " + itemName + " slides into the backpack.");
            items.Add(itemName, item);
            return true;
        }
        else
        {
            // Is er genoeg ruimte in de Inventory?
            // Past het Item?
            // Zet Item in de Dictionary
            // Return true/false voor succes/mislukt
            Console.WriteLine("The " + itemName + " doesn't fit.");
            return false;
        }
    }
    public Item Get(string itemName)
    {
        // TODO implementeer:
        // Zoek Item in de Dictionary
        // Verwijder Item uit Dictionary (als gevonden)
        // Return Item of null
        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];
            Console.WriteLine(item);
            items.Remove(itemName);
            return item;
        }
        else
        {
            return null;
        }
    }
    public int TotalWeight()
    {
        int total = 0;
        // TODO implementeer:
        // Loop door alle items
        // Tel alle gewichten op
        return total;
    }
    public int FreeWeight()
    {
        int yaba = 0;
        // TODO implementeer:
        // Vergelijk MaxWeight en TotalWeight()
        return yaba;
    }
}