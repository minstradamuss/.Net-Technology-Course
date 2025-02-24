using System;

class Fighter
{
    private int iDamage;

    public int FighterHealth { get; set; }
    public int FighterDamage { get; set; }
    public int WeaponStatus { get; set; }

    private void LogStatus(string name, int age, int health, int damage, int weaponStatus)
    {
        // вынесем в отдельную переменную
        var log = $"name:{name}, age:{age}, health:{health}, damage:{damage}, weaponStatus:{weaponStatus}";
        Console.WriteLine(log);
    }

    public int GetDamage()
    {
        // Weapon_Status * 5 
        // Console.WriteLine($"Get Damage {iDamage}"); 
        return iDamage;
    }

    private void PerformAttack()
    {
        Console.WriteLine("Go Attack!");
        // TO DO: implement attack
    }

    public void Attack()
    {
        try
        {
            PerformAttack();
        }
        // просто логируем ошибку
        catch (Exception e)
        {
            Console.WriteLine($"Go Attack Exception: {e.Message}");
        }
    }
}
