using System.Text;
using Lab4.Sensors;

ISensor tempSensor = new TemperatureSensor();
ISensor pressureSensor = new PressureSensor();

tempSensor.AddReading(22.5);
tempSensor.AddReading(25.1);
tempSensor.AddReading(19.8);

pressureSensor.AddReading(101.3);
pressureSensor.AddReading(99.8);
pressureSensor.AddReading(105.4);

SensorAnalyzer tempAnalyzer = new(tempSensor);
SensorAnalyzer pressureAnalyzer = new(pressureSensor);

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine($"{tempSensor.Name}: Середнє = {tempAnalyzer.GetAverage():F2}, Відхилення = {(tempAnalyzer.HasDeviation(20, 26) ? "присутнє" : "немає")}");
Console.WriteLine($"{pressureSensor.Name}: Середнє = {pressureAnalyzer.GetAverage():F2}, Відхилення = {(pressureAnalyzer.HasDeviation(100, 104) ? "присутнє" : "немає")}");
