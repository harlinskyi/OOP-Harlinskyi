using System;

namespace lab21v7;

public interface IStorageStrategy
{
    decimal CalculateCost(double dataGb, int usersCount);
}