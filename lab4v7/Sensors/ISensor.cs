namespace Lab4.Sensors
{
    public interface ISensor
    {
        string Name { get; }
        double[] Readings { get; }

        double GetLastReading();
        void AddReading(double value);
    }
}
