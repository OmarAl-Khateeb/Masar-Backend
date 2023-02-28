using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems, string appUserId, int gymId, decimal subtotal, DeliveryMethod deliveryMethod)
        {
            AppUserId = appUserId;
            GymId = gymId;
            OrderItems = orderItems;
            Subtotal = subtotal;
            DeliveryMethod = deliveryMethod;
        }

        public string AppUserId { get; set; }
        public int GymId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}