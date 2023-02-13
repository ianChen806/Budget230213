namespace Budget230213;

public static class DateTimeExtension
{
    public static int DaysInMonth(this DateTime date)
    {
        return DateTime.DaysInMonth(date.Year, date.Month);
    }

    public static DateTime LastDay(this DateTime current)
    {
        var lastDay = new DateTime(current.Year, current.Month, current.DaysInMonth());
        return lastDay;
    }

    public static DateTime FirstDay(this DateTime current)
    {
        var dateTime = new DateTime(current.Year, current.Month, 1);
        return dateTime;
    }
}