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
        return _repository.GetAll().FirstOrDefault(r => r.YearMonth == date.ToString("yyyyMM")) ?? new Budget();
    }

    private IEnumerable<Period> SplitPeriod(DateTime startTime, DateTime endTime)
    {
        var current = startTime;
        do
        {
            var lastDay = new DateTime(current.Year, current.Month, current.DaysInMonth());
            if (endTime <= lastDay)
            {
                yield return new Period(current, endTime);
                break;
            }
            else
            {
                yield return new Period(current, lastDay);
            }

            current = new DateTime(current.Year, current.Month, 1).AddMonths(1);
        } while (true);
    }
}