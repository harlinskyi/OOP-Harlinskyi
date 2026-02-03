namespace lab23v7.Refactored
{
    // ISP: Розділяємо інтерфейси для кожної функції
    public interface IEngine { void Start(); }
    public interface IRadio { void Play(); }
    public interface IGPS { void Locate(); }

    // Реалізації
    public class ModernEngine : IEngine
    {
        public void Start() => Console.WriteLine("[Refactored] Двигун працює тихо.");
    }

    public class SonyRadio : IRadio
    {
        public void Play() => Console.WriteLine("[Refactored] Грає якісна музика Sony.");
    }

    public class GarminGPS : IGPS
    {
        public void Locate() => Console.WriteLine("[Refactored] Точні координати Garmin отримано.");
    }

    // DIP: Клас залежить від абстракцій. DI: Залежності передаються в конструктор.
    public class CarComputer
    {
        private readonly IEngine _engine;
        private readonly IRadio _radio;
        private readonly IGPS _gps;

        public CarComputer(IEngine engine, IRadio radio, IGPS gps)
        {
            _engine = engine;
            _radio = radio;
            _gps = gps;
        }

        public void RunSystem()
        {
            _engine.Start();
            _gps.Locate();
            _radio.Play();
            Console.WriteLine("Системи працюють згідно з новими принципами.");
        }
    }
}