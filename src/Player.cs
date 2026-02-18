using System.Collections;

class Player
{
    // auto property
    public Room CurrentRoom { get; set; }

    // fields
    public int health;
    private Inventory backpack;
    public Inventory Backpack
    {
        get { return backpack; }
    }
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
        if (this.health <= 0)
        {
            return false;
        }
        return true;
    }
    public void ShowInventory()
    {
        backpack.Show();
    }

    public bool TakeFromChest(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);
        if (item == null)
        {
            Console.WriteLine("take what?");
            return false;
        }
        if (backpack.Put(itemName, item))
        {
            Console.WriteLine("The " + itemName + " slides into the backpack");
            return true;
        }
        else
        {
            Console.WriteLine("The " + itemName + " doesn't fit.");
            return false;
        }
    }
    public bool DropToChest(string itemName)
    {
        Item item = backpack.Get(itemName);
        if (item != null)
        {
            CurrentRoom.Chest.Put(itemName, item);
            Console.WriteLine("You gently lay down the " + itemName + ".");
            return true;
        }
        else
        {
            Console.WriteLine("You don't have a(an) " + itemName + " in your backpack.");
            return false;
        }
    }
    public void Use(string itemName, Command command)
    {
        switch (itemName)
        {
            case "inconspicuous_ring":
            Console.WriteLine("You put on the ring and are immediately overwhelmed by its energy that you start to burst at the seams.");
            Damage(9999999);
            break;
        }
    }
}