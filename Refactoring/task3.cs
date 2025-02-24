class fighter
{
    private int iDamage;
    public string sName;

    public int fighterHealth
    { get; set; }

    public int fighterDamage
    { get; set; }

    public int Weapon_Status
    { get; set; }

    void logStatus(string name, int age, int health, int damage, int weaponStatus)
    {
        Console.WriteLine($"name:{name}, age:{age}, health:{health}, damage:{damage}, 
        weaponStatus:{ weaponStatus}"); 
    }
    public int GetDamage()
    {
        // Weapon_Status * 5 
        // Console.WriteLine($"Get Damage {iDamage}"); 
        return iDamage;
    }
    void atck()
    {
        Console.WriteLine("Go Attack!");
        // TO DO: implement attack 
    }
    public void Attack()
    {
        try
        {
            atck();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Go Attack Exception: {e}");
            throw e;
        }
    }
}