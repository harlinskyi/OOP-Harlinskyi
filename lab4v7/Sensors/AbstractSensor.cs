namespace Lab4.Sensors
{
    public abstract class AbstractSensor : ISensor
    {
        protected List<double> readings = new List<double>();

        public string Name { get; protected set; }
        public double[] Readings => readings.ToArray();

        public double GetLastReading()
        {
            return readings.LastOrDefault();
        }

        public void AddReading(double value)
        {
            readings.Add(value);
        }
    }
}
