using System;

namespace lab21v7;

public class PersonalPlanStrategy : IStorageStrategy
{
    public decimal CalculateCost(double dataGb, int usersCount) =>
        (decimal)dataGb * StorageConstants.PersonalGbRate +
        (usersCount * StorageConstants.PersonalUserRate);
}