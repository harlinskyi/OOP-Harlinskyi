using System;

namespace lab21v7;

public static class StoragePlanFactory
{
    public static IStorageStrategy CreatePlan(string choice)
    {
        return choice switch
        {
            "1" => new PersonalPlanStrategy(),
            "2" => new BusinessPlanStrategy(),
            "3" => new EnterprisePlanStrategy(),
            "4" => new PremiumPlanStrategy(), // Наша демонстрація OCP
            _ => throw new ArgumentException("Невірний вибір. Будь ласка, оберіть цифру від 1 до 4.")
        };
    }
}