using System;

namespace lab24.Strategies;

public class SquareOperationStrategy : INumericOperationStrategy
{
    public string Name => "Квадрат";
    public double Execute(double value) => value * value;
}