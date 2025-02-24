public class gMethods
{
    public string Name;

    private int price;
    private int amount;
    private string platform;

    public void PrintPack()
    {
        this.PrintBanner();

        // Print details. 
        Console.WriteLine("name: " + this.name);
        Console.WriteLine("amount: " + this.GetOutstanding());
        Console.WriteLine("price: " + this.price);
        Console.WriteLine("platform: " + platform);
    }

    float GetAmnt()
    {
        if ((platform.ToUpper().IndexOf("PC") > -1) &&
             (Name.ToUpper().IndexOf("XX") > -1) && amount > 0)
            return amount * 0.956;

        double temp = amount * price;
        Console.WriteLine(temp);

        temp = 0.8 * amount * price;
        Console.WriteLine(temp);
        return -1;
    }
}