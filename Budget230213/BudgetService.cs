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
        var periods = SplitPeriod(startTime, endTime);
        return periods.Select(CalBudget).Sum();
    }

    private decimal CalBudget(Period period)
    {
        var budget = QueryBudget(period.Date);
        return budget.Amount / period.Date.DaysInMonth() * (decimal)period.TotalDays();
    }

    private Budget QueryBudget(DateTime date)
    {
        return _repository.GetAll().First(r => r.YearMonth == date.ToString("yyyyMM"));
    }

    private List<Period> SplitPeriod(DateTime startTime, DateTime endTime)
    {
        var lastDay = new DateTime(startTime.Year, startTime.Month, startTime.DaysInMonth());
        if (endTime <= lastDay)
        {
            return new List<Period> { new(startTime, endTime) };
        }

        return new List<Period>
        {
            new(startTime, lastDay),
            new(new DateTime(endTime.Year, endTime.Month, 1), endTime)
        };
    }
}