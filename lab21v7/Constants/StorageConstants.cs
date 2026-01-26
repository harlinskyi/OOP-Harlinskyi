using System;

namespace lab21v7;

public static class StorageConstants
{
    // Персональний план
    public const decimal PersonalGbRate = 0.5m;
    public const decimal PersonalUserRate = 10m;

    // Бізнес план
    public const decimal BusinessGbRate = 0.3m;
    public const decimal BusinessUserRate = 50m;

    // Ентерпрайз план
    public const decimal EnterpriseBaseRate = 1000m;
    public const decimal EnterpriseGbRate = 0.1m;
    public const decimal EnterpriseUserRate = 30m;

    // Преміум план (Демо OCP)
    public const decimal PremiumBaseRate = 500m;
    public const decimal PremiumGbRate = 0.2m;
}