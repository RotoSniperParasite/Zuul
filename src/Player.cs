class Player
{
    // auto property
    public Room CurrentRoom { get; set; }

    // fields
    public int health;
    private Inventory backpack;

    // constructor
    public Player()
    {
        CurrentRoom = null;
        health = 100;
        backpack = new Inventory(25);
    }

    // methods
    public void Damage(int amount)
    {
        health -= amount;
    }
    // speler verliest health

    public void Heal(int amount)
    {
        health += amount;
    }
    // speler krijgt health 

    public bool IsAlive()
    {
        if (this.health == 0)
        {
            return false;
        }
        return true;
    }
    public string ShowInventoryBackpack()
    {
        return backpack.Show();
    }

    public bool TakeFromChest(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);
        if (backpack.FreeWeight() >= item.Weight)
        {
            backpack.Put(itemName, item);
            Console.WriteLine("The " + itemName + "slides into the backpack");
            return true;
        }
        else
        {
            Console.WriteLine($"The " + itemName + " doesn't fit.");
            return false;
        }

    }
    public bool DropToChest(string itemName)
    {
        Item item = backpack.Get(itemName);
        if(item == null)
        {
            CurrentRoom.Chest.Put(itemName, item);
            Console.WriteLine("You gently lay down the " + itemName + ".");
            return true;
        }
        else
        {
            Console.WriteLine("You don't have a(an) " + itemName + "in your backpack.");
            return false;
        }
    }
}