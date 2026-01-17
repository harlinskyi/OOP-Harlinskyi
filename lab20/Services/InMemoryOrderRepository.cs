using System;
using lab20.Interfaces;
using lab20.Models;

namespace lab20.Services
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine($"[Database] Order #{order.Id} for {order.CustomerName} saved to memory.");
        }

        public Order GetById(int id) => null; 
    }
}