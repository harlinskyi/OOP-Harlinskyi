using System;

namespace lab25.Processing;

public class CompressDataStrategy : IDataProcessorStrategy
{
    public string Process(string data) => $"Compressed_Archive({data})";
}