using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.WebApi.Models
{
    public class AddOrderRequest
    {
        [Required]
        public List<OrderProductRequest> Products { get; set; }
    }
    public class OrderProductRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }

}
