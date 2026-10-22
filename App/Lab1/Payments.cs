namespace App.Lab1;

public enum PaymentsPlan
{
    Differentiated,
    Annuity
}

public static class Payments
{
    public static decimal CalculateTotalPayments(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount)
    {
        if (rate == 0)
        {
            return amount;
        }
        decimal result = 0;
        if (plan == PaymentsPlan.Annuity)
        {
            decimal mounthlyRate = rate / 12 / 100;
            double pow = Math.Pow((double)(1 + mounthlyRate), monthsCount);
            decimal monthlyPayment = amount * mounthlyRate * (decimal)pow / ((decimal)pow - 1);
            result = monthlyPayment * monthsCount;
        }
        else
        {
            decimal mounthlyRate = rate / 12 / 100;
            decimal mainDebt = amount / monthsCount;
            for (int i = 0; i < monthsCount; i++)
            {
                decimal currentAmount = amount;
                currentAmount -= i * (mainDebt);
                result += mainDebt + currentAmount *  mounthlyRate;
            }
        }

        return Math.Round(result, 2);
    }
}