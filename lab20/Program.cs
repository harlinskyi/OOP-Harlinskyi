using System;
using lab20.Models;
using lab20.Services;
using lab20.Interfaces;

namespace lab20
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrderValidator validator = new OrderValidator();
            IOrderRepository repository = new InMemoryOrderRepository();
            IEmailService emailService = new ConsoleEmailService();

            var orderService = new OrderService(validator, repository, emailService);

            var validOrder = new Order(101, "Кирило", 1250.00m);
            orderService.ProcessOrder(validOrder);

            var invalidOrder = new Order(102, "Тест", -5.00m);
            orderService.ProcessOrder(invalidOrder);

            Console.WriteLine("Тицьни будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}