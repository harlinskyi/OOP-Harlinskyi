using System;

namespace lab25.Processing;

public class DataContext
{
    private IDataProcessorStrategy _strategy;
    public DataContext(IDataProcessorStrategy strategy) => _strategy = strategy;
    public void SetStrategy(IDataProcessorStrategy strategy) => _strategy = strategy;
    public string Execute(string data) => _strategy.Process(data);
}