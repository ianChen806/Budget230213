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
        return 100;
    }
}