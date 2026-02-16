using System;

namespace lab24.Strategies;

public interface INumericOperationStrategy
{
    string Name { get; }
    double Execute(double value);
}