using Xunit;

public class TipCalculatorTests
{
    // корректный расчет чаевых
    [Fact]
    public void CalculateTotal_ValidInput_ReturnsCorrectTotal()
    {
        var model = new TipCalculatorModel(100, 10);
        decimal result = model.CalculateTotal();
        Assert.Equal(110, result);
    }

    // отрицательная сумма счета
    [Fact]
    public void Constructor_NegativeBillAmount_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new TipCalculatorModel(-50, 10));
    }

    // процент чаевых больше 100%
    [Fact]
    public void Constructor_TipPercentageMoreThan100_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new TipCalculatorModel(100, 150));
    }

    // отрицательный процент чаевых
    [Fact]
    public void Constructor_NegativeTipPercentage_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new TipCalculatorModel(100, -10));
    }

    // ввод корректного числа
    [Fact]
    public void GetInput_ValidNumber_ReturnsParsedValue()
    {
        var view = new TipCalculatorView();
        var input = new StringReader("200");
        Console.SetIn(input);

        decimal result = view.GetInput("Введите сумму счета: ");

        Assert.Equal(200, result);
    }

    // ввод некорректного числа
    [Fact]
    public void GetInput_InvalidNumber_ThrowsFormatException()
    {
        var view = new TipCalculatorView();
        var input = new StringReader("abc");
        Console.SetIn(input);

        Assert.Throws<FormatException>(() => view.GetInput("Введите сумму счета: "));
    }

    // ввод пустого значения
    [Fact]
    public void GetInput_EmptyInput_ThrowsFormatException()
    {
        var view = new TipCalculatorView();
        var input = new StringReader("\n");
        Console.SetIn(input);

        Assert.Throws<FormatException>(() => view.GetInput("Введите сумму счета: "));
    }

    // проверка корректного вывода сообщений
    [Fact]
    public void ShowMessage_DisplaysCorrectMessage()
    {
        var view = new TipCalculatorView();
        var output = new StringWriter();
        Console.SetOut(output);

        view.ShowMessage("Тестовое сообщение");

        Assert.Contains("Тестовое сообщение", output.ToString());
    }
}
