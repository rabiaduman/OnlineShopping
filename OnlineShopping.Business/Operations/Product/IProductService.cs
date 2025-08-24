using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopping.Business.Operations.Product.Dtos;
using OnlineShopping.Business.Types;

namespace OnlineShopping.Business.Operations.Product
{
    public interface IProductService
    {
        Task<ServiceMessage> AddProduct(AddProductDto product);

        Task<List<ProductDto>> GetAllProducts();

        Task<ProductDto> GetProductById(int id);

        Task<ServiceMessage> AdjustProductPrice(int id, decimal changeTo);

        Task<ServiceMessage> AdjustProductStockQuantity(int id, int changeTo);

        Task<ServiceMessage> DeleteProduct(int id);

        Task<ServiceMessage> UpdateProduct(UpdateProductDto product);

    }
}
