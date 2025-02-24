using System;

public class gMethods
{
    public string Name { get; set; }
    private int price;
    private int amount;
    private string platform;

    // êîíñòàíòû âìåñòî ìàãè÷åñêèõ ÷èñåë
    private const float amountCoefficient = 0.956;
    private const double coefficient = 0.8;

    // ÷åðåç ìåòîäû
    public void PrintPack()
    {
        this.PrintBanner();
        this.PrintDetails();
    }

    // âûíîñèì ýòî â îòäåëüíûé ìåòîä
    private void PrintDetails()
    {
        Console.WriteLine("name: " + this.Name);
        Console.WriteLine("amount: " + this.GetAmount());
        Console.WriteLine("price: " + this.price);
        Console.WriteLine("platform: " + platform);
    }

    // ïðîâåðêà óñëîâèé îòäåëüíûì ìåòîäîì
    private bool IsPlatformAndNameValid()
    {
        return platform.ToUpper().Contains("PC") &&
               Name.ToUpper().Contains("XX") &&
               amount > 0;
    }

    private float GetAmount()
    {
        if (IsPlatformAndNameValid())
            return amount * amountCoefficient;

        LogPriceCalculations();
        return -1;
    }

    // Ìåòîä äëÿ ëîãèðîâàíèÿ âû÷èñëåíèé öåíû
    private void LogPriceCalculations()
    {
        // ðàçíûå ïåðåìåííûå äëÿ ðàçíûõ çíà÷åíèé
        // êàæäàÿ ïåðåìåííàÿ îòâå÷àåò òîëüêî çà îäíó îïðåäåë¸ííóþ âåùü
        double finalPrice = amount * price;
        Console.WriteLine(finalPrice);

        double finalPriceWithCoefficient = coefficient * amount * price;
        Console.WriteLine(finalPriceWithCoefficient);
    }
}
