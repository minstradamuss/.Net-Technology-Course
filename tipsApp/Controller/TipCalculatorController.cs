public class TipCalculatorController
{
    private readonly TipCalculatorView _view;

    public TipCalculatorController(TipCalculatorView view)
    {
        _view = view;
    }

    public void Run()
    {
        try
        {
            decimal billAmount = _view.GetInput("Введите сумму счета: ");
            decimal tipPercentage = _view.GetInput("Введите процент чаевых: ");

            TipCalculatorModel model = new TipCalculatorModel(billAmount, tipPercentage);
            decimal totalAmount = model.CalculateTotal();

            _view.ShowMessage($"Общая сумма к оплате: {totalAmount} р.");
        }
        catch (Exception ex)
        {
            _view.ShowMessage($"Ошибка: {ex.Message}");
        }
    }
}
