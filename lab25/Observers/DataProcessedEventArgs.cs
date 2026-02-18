using System;

namespace lab25.Observers;

public class DataProcessedEventArgs : EventArgs
{
    public string Result { get; set; } = string.Empty;
}