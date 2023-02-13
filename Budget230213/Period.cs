namespace Budget230213;

public class Period
{
    private readonly DateTime _startTime;
    private readonly DateTime _endTime;

    public Period(DateTime startTime, DateTime endTime)
    {
        _startTime = startTime;
        _endTime = endTime;
    }

    public  double TotalDays()
    {
        return (_endTime - _startTime).TotalDays + 1;
    }
}