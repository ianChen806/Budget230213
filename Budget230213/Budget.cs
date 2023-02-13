namespace Budget230213;

public class Budget
{
    public decimal Amount { get; set; }

    public string YearMonth { get; set; } = null!;

    public decimal TotalAmount(Period period)
    {
        return Amount / period.Date.DaysInMonth() * (decimal)period.TotalDays();
    }
}