using System;

namespace lab21v7;

public class PremiumPlanStrategy : IStorageStrategy
{
    public decimal CalculateCost(double dataGb, int usersCount)
    {
        return 500m + (decimal)dataGb * 0.2m;
    }
}