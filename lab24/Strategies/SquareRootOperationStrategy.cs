using System;

namespace lab24.Strategies;

public class SquareRootOperationStrategy : INumericOperationStrategy
{
    public string Name => "Квадратний корінь";
    public double Execute(double value) => Math.Sqrt(value);
}