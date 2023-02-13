namespace Budget230213;

public class BudgetService
{
    private readonly IBudgetRepository _repository;

    public BudgetService(IBudgetRepository repository)
    {
        _repository = repository;
    }

    public double Query(DateTime startTime, DateTime endTime)
    {
        var totalDays = (endTime - startTime).TotalDays + 1;
        var daysInMonth = DateTime.DaysInMonth(startTime.Year, startTime.Month);
        var budget = _repository.GetAll().First(r => r.YearMonth == startTime.ToString("yyyyMM"));
        return (budget.Amount / daysInMonth) * totalDays;
    }
}