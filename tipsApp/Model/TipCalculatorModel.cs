public class TipCalculatorModel
{
    public decimal BillAmount { get; private set; }
    public decimal TipPercentage { get; private set; }

    public TipCalculatorModel(decimal billAmount, decimal tipPercentage)
    {
        if (billAmount < 0)
            throw new ArgumentException("Сумма счета не может быть отрицательной.");

        if (tipPercentage < 0 || tipPercentage > 100)
            throw new ArgumentException("Процент чаевых должен быть в пределах от 0 до 100.");

        BillAmount = billAmount;
        TipPercentage = tipPercentage;
    }

    public decimal CalculateTotal()
    {
        return BillAmount + (BillAmount * (TipPercentage / 100));
    }
}
