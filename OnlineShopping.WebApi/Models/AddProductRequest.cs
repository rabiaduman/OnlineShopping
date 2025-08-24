using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.WebApi.Models
{
    public class AddProductRequest
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat bilgisi 0'dan küçük olamaz.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Stok miktarı 0'dan büyük olmalı.")]
        public int StockQuantity { get; set; }
    }
}
