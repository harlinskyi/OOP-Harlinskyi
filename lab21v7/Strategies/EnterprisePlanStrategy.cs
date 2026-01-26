using System;

namespace lab21v7;

public class EnterprisePlanStrategy : IStorageStrategy
{
    public decimal CalculateCost(double dataGb, int usersCount) =>
        StorageConstants.EnterpriseBaseRate +
        ((decimal)dataGb * StorageConstants.EnterpriseGbRate) +
        (usersCount * StorageConstants.EnterpriseUserRate);
}