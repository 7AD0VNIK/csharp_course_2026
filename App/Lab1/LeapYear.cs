namespace App.Lab1;

public static class LeapYear
{
    public static bool IsLeapYear(int year) 
    {
        if (year % 4 == 0)
        {
            return year % 100 != 0 || year % 400 == 0;
        }

        return false;
    }
}