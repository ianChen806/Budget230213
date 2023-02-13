namespace Budget230213;

public static class DateTimeExtension
{
    public static int DaysInMonth(this DateTime date)
    {
        return DateTime.DaysInMonth(date.Year, date.Month);
    }
}

public class BudgetService
{
    private readonly IBudgetRepository _repository;

    public BudgetService(IBudgetRepository repository)
    {
        _repository = repository;
    }

    public decimal Query(DateTime startTime, DateTime endTime)
    {
        var period = new Period(startTime, endTime);
        var budget = QueryBudget(startTime);
        return budget.Amount / startTime.DaysInMonth() * (decimal)period.TotalDays();
    }

    private Budget QueryBudget(DateTime date)
    {
        return _repository.GetAll().First(r => r.YearMonth == date.ToString("yyyyMM"));
    }
}