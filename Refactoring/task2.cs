using System;

public class gMethods
{
    public string Name { get; set; }
    private int price;
    private int amount;
    private string platform;

    // ��������� ������ ���������� �����
    private const float amountCoefficient = 0.956f;
    private const double coefficient = 0.8;

    // ����� ������
    public void PrintPack()
    {
        this.PrintBanner();
        this.PrintDetails();
    }

    // ������� ��� � ��������� �����
    private void PrintDetails()
    {
        Console.WriteLine("name: " + this.Name);
        Console.WriteLine("amount: " + this.GetAmount());
        Console.WriteLine("price: " + this.price);
        Console.WriteLine("platform: " + platform);
    }

    // �������� ������� ��������� �������
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

    // ����� ��� ����������� ���������� ����
    private void LogPriceCalculations()
    {
        // ������ ���������� ��� ������ ��������
        // ������ ���������� �������� ������ �� ���� ����������� ����
        double finalPrice = amount * price;
        Console.WriteLine(finalPrice);

        double finalPriceWithCoefficient = coefficient * amount * price;
        Console.WriteLine(finalPriceWithCoefficient);
    }
}
