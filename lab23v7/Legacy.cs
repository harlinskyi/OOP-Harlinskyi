namespace lab23v7.Legacy
{
    // Низькорівневі модулі без абстракцій
    public class EngineUnit
    {
        public void Start() => Console.WriteLine("[Legacy] Двигун запущено.");
    }

    public class RadioTuner
    {
        public void PlayMusic() => Console.WriteLine("[Legacy] Радіо грає.");
    }

    public class GPSModule
    {
        public void GetCoordinates() => Console.WriteLine("[Legacy] Маршрут побудовано.");
    }

    // Головний клас - ПОРУШУЄ DIP (сам створює об'єкти)
    public class CarComputer
    {
        private EngineUnit _engine = new EngineUnit();
        private RadioTuner _radio = new RadioTuner();
        private GPSModule _gps = new GPSModule();

        public void StartCar()
        {
            _engine.Start();
            _gps.GetCoordinates();
            _radio.PlayMusic();
            Console.WriteLine("Авто готове до поїздки.");
        }
    }
}