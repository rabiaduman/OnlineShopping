using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopping.Business.Operations.Order.Dtos;
using OnlineShopping.Business.Types;

namespace OnlineShopping.Business.Operations.Order
{
    public interface IOrderService
    {
        Task<ServiceMessage> AddOrder(AddOrderDto order);

        Task<OrderDto> GetOrderById(int id);

        Task<List<OrderDto>> GetOrders();

        Task<ServiceMessage> DeleteOrder(int id);
    }
}
