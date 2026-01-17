using System;
using lab20.Interfaces;
using lab20.Models;

namespace lab20.Services
{
    public class OrderService
    {
        private readonly IOrderValidator _validator;
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;

        public OrderService(IOrderValidator validator, IOrderRepository repository, IEmailService emailService)
        {
            _validator = validator;
            _repository = repository;
            _emailService = emailService;
        }

        public void ProcessOrder(Order order)
        {
            Console.WriteLine($"--- Starting process for Order #{order.Id} ---");

            if (!_validator.IsValid(order))
            {
                Console.WriteLine($"[Error] Order #{order.Id} failed validation (Invalid Amount).");
                return;
            }

            _repository.Save(order);
            
            order.Status = OrderStatus.Processed;
            
            _emailService.SendOrderConfirmation(order);

            Console.WriteLine($"[Success] Order #{order.Id} processed.\n");
        }
    }
}