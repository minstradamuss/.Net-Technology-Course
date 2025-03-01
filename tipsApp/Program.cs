namespace tipsApp;

public class Program
{
    static void Main()
    {
        var controller = CreateController();
        controller.Run();
    }

    public static TipCalculatorController CreateController()
    {
        return new TipCalculatorController(new TipCalculatorView());
    }
}
