namespace Budget230213;

public class Period
{
    private readonly DateTime _endTime;
    private readonly DateTime _startTime;

    public Period(DateTime startTime, DateTime endTime)
    {
        _startTime = startTime;
        _endTime = endTime;
    }

    public DateTime Date => _startTime.Date;

    public double TotalDays()
    {
        return (_endTime - _startTime).TotalDays + 1;
    }
}