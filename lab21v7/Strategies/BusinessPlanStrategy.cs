using System;

namespace lab21v7;

public class BusinessPlanStrategy : IStorageStrategy
{
    public decimal CalculateCost(double dataGb, int usersCount) =>
        (decimal)dataGb * StorageConstants.BusinessGbRate +
        (usersCount * StorageConstants.BusinessUserRate);
}