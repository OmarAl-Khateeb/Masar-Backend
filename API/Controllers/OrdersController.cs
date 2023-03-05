using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Infrastructue.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<StoreContext> _unitOfWork;
        public OrdersController(IMapper mapper, IUnitOfWork<StoreContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderCDto orderDto)
        {
            //Get Items from product repo
            var items = new List<OrderItem>();
            foreach (var Item in orderDto.OrderItems)
            {
                var productItem = await _unitOfWork.Repository<Product, StoreContext>().GetByIdAsync(Item.ProductId);
                
                if (productItem == null) return BadRequest(new ApiResponse(400, "Product not found"));

                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, Item.Quantity);
                items.Add(orderItem);
            }
        
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, StoreContext>().GetByIdAsync(orderDto.DeliveryMethodId);
            
            var subtotal = items.Sum(item => item.Price*item.Quantity);

            var order = new Order(items, User.GetUserId(), User.GetGymId(), subtotal, User.GetUserId(), deliveryMethod);

            _unitOfWork.Repository<Order, StoreContext>().Add(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders(string appUserId, int? gymId)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(appUserId, gymId);

            var orders = await  _unitOfWork.Repository<Order, StoreContext>().ListAsync(spec);
            
            if (orders == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }

        [HttpGet("User")]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetUserOrders()
        {
            var spec = new OrderWithItemsAndOrderingSpecification(User.GetUserId(), null);

            var orders = await  _unitOfWork.Repository<Order, StoreContext>().ListAsync(spec);
            
            if (orders == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _unitOfWork.Repository<DeliveryMethod, StoreContext>().ListAllAsync());
        }
        
    }
}