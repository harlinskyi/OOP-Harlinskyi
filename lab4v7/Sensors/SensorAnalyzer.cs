namespace Lab4.Sensors
{
    public class SensorAnalyzer
    {
        private readonly ISensor sensor;

        public SensorAnalyzer(ISensor sensor)
        {
            this.sensor = sensor;
        }
        public double GetAverage()
        {
            if (!sensor.Readings.Any()) return 0;
            return sensor.Readings.Average();
        }
        public bool HasDeviation(double min, double max)
        {
            return sensor.Readings.Any(r => r < min || r > max);
        }
    }
}
