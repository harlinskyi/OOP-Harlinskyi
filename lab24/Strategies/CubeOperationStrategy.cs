using System;

namespace lab24.Strategies;

public class CubeOperationStrategy : INumericOperationStrategy
{
    public string Name => "Куб";
    public double Execute(double value) => value * value * value;
}