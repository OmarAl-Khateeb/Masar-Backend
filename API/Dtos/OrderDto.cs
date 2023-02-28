using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace API.Dtos
{
    public class OrderCDto
    {
        public IReadOnlyList<OrderCItemDto> OrderItems { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string PaymentIntentId { get; set; }
    }

    
    public class OrderDto
    {        
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int GymId { get; set; }
        public DateTime OrderDate { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string DeliveryMethod { get; set; }
        public string Status { get; set; }
    }
}