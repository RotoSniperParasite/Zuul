class Player
{
    // auto property
    public Room CurrentRoom { get; set; }

    // fields
    public int health;

    // constructor
    public Player()
    {
        CurrentRoom = null;
        health = 100;
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
        if(this.health == 0)
        {
            return false;
        }
        return true;
    }
    // checkt of speler nog leeft
}