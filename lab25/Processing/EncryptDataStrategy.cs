using System;

namespace lab25.Processing;

public class EncryptDataStrategy : IDataProcessorStrategy
{
    public string Process(string data) => $"Encrypted_Content({data})";
}