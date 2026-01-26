using System;

namespace lab21v7;

public class CloudStorageService
{
    public decimal GetTotalCost(double dataGb, int usersCount, IStorageStrategy strategy)
    {
        if (strategy == null) throw new ArgumentNullException(nameof(strategy));
        return strategy.CalculateCost(dataGb, usersCount);
    }
}