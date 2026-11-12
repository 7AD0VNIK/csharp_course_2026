namespace App.Lab1;

public static class Prices
{
    public static string TheFinalTouch(string str, bool isShorNotation, bool isFirstCapital)
    {
        if (isShorNotation)
        {
            return isFirstCapital ? "Руб." : "руб.";
        }

        if (isFirstCapital)
        {
            return str[0].ToString().ToUpper() + str.Substring(1);
        }

        return str;
    }
    public static string GetCurrencyAlias(int price, bool isShorNotation, bool isFirstCapital)
    {
        var lastTwo = price % 100;
        var lastOne = price % 10;
        string res = "";
        if (lastTwo <= 14 && lastTwo >= 11)
        {
            res += "рублей";
        }
        else
        {
            if (lastOne == 1)
            {
                res += "рубль";
            }

            if (lastOne == 2 || lastOne == 3 || lastOne == 4)
            {
                res += "рубля";
            }

            if (lastOne == 5 || lastOne == 6 || lastOne == 7 || lastOne == 8 || lastOne == 9 || lastOne == 0)
            {
                res += "рублей";
            }
        }
        
        return TheFinalTouch(res, isShorNotation, isFirstCapital);
    }
}