using lab20.Interfaces;
using lab20.Models;

namespace lab20.Services
{
    public class OrderValidator : IOrderValidator
    {
        public bool IsValid(Order order)
        {
            return order.TotalAmount > 0;
        }
    }
}