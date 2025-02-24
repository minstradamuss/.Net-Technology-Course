using System;

public class gMethods
{
    public string Name { get; set; }
    private int price;
    private int amount;
    private string platform;

    // константы вместо магических чисел
    private const float amountCoefficient = 0.956f;
    private const double coefficient = 0.8;

    // через методы
    public void PrintPack()
    {
        this.PrintBanner();
        this.PrintDetails();
    }

    // выносим это в отдельный метод
    private void PrintDetails()
    {
        Console.WriteLine("name: " + this.Name);
        Console.WriteLine("amount: " + this.GetAmount());
        Console.WriteLine("price: " + this.price);
        Console.WriteLine("platform: " + platform);
    }

    // проверка условий отдельным методом
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

    // Метод для логирования вычислений цены
    private void LogPriceCalculations()
    {
        // разные переменные для разных значений
        // каждая переменная отвечает только за одну определённую вещь
        double finalPrice = amount * price;
        Console.WriteLine(finalPrice);

        double finalPriceWithCoefficient = coefficient * amount * price;
        Console.WriteLine(finalPriceWithCoefficient);
    }
}
