namespace Budget230213;

public class BudgetService
{
    private readonly IBudgetRepository _repository;

    public BudgetService(IBudgetRepository repository)
    {
        _repository = repository;
    }

    public decimal Query(DateTime startTime, DateTime endTime)
    {
        return Periods(startTime, endTime).Select(CalBudget).Sum();
    }

    private decimal CalBudget(Period period)
    {
        return QueryBudget(period.Date).TotalAmount(period);
    }

    private IEnumerable<Period> Periods(DateTime startTime, DateTime endTime)
    {
        for (var current = startTime; current <= endTime; current = current.FirstDay().AddMonths(1))
        {
            var lastDay = current.LastDay();
            if (endTime <= lastDay)
            {
                yield return new Period(current, endTime);
            }
            else
            {
                yield return new Period(current, lastDay);
            }
        }
    }

    private Budget QueryBudget(DateTime date)
    {
        return _repository.GetAll().FirstOrDefault(r => r.YearMonth == date.ToString("yyyyMM")) ?? new Budget();
    }
}