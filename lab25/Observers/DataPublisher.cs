using System;

namespace lab25.Observers;

public class DataPublisher
{
    public event EventHandler<DataProcessedEventArgs>? DataProcessed;

    public void Publish(string result)
    {
        DataProcessed?.Invoke(this, new DataProcessedEventArgs { Result = result });
    }
}