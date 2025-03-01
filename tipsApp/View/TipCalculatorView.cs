public class TipCalculatorView
{
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public decimal GetInput(string prompt)
    {
        Console.Write(prompt);
        if (decimal.TryParse(Console.ReadLine(), out decimal result))
        {
            return result;
        }
        throw new FormatException("Некорректный ввод. Введите число.");
    }
}
