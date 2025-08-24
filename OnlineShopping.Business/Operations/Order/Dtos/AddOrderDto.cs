using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Operations.Order.Dtos
{
    public class AddOrderDto
    {
        public int UserId { get; set; }
        public List<OrderProductDto> Products { get; set; }
    }
    public class OrderProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
