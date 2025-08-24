using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Business.Operations.Product.Dtos;
using OnlineShopping.Business.Types;
using OnlineShopping.Data.Entities;
using OnlineShopping.Data.Repositories;
using OnlineShopping.Data.UnitOfWork;

namespace OnlineShopping.Business.Operations.Product
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductEntity> _repository;

        public ProductManager(IUnitOfWork unitOfWork, IRepository<ProductEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ServiceMessage> AddProduct(AddProductDto product)
        {
            var hasProduct = _repository.GetAll(x => x.ProductName.ToLower() == product.ProductName.ToLower()).Any();

            if (hasProduct)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu ürün zaten mevcut."
                };
            }

            var productEntity = new ProductEntity
            {
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            _repository.Add(productEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Ürün ekleme sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };
        }

        public async Task<ServiceMessage> AdjustProductPrice(int id, decimal changeTo)
        {
            var product = _repository.GetById(id);

            if (product is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu id ile eşleşen ürün bulunamadı."
                };
            }

            product.Price = changeTo;

            _repository.Update(product);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception) 
            {
                throw new Exception("Ürünün fiyatı değiştirilirken bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };

        }

        public async Task<ServiceMessage> AdjustProductStockQuantity(int id, int changeTo)
        {
            var product = _repository.GetById(id);

            if (product is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu id ile eşleşen ürün bulunamadı."
                };
            }

            product.StockQuantity = changeTo;

            _repository.Update(product);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Stok adedi değiştirilirken bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };
        }

        public async Task<ServiceMessage> DeleteProduct(int id)
        {
            var product = _repository.GetById(id);

            if (product is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Silinmek istenen ürün bulunamadı."
                };
            }

            _repository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Silme işlemi sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };

        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _repository.GetAll()
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    StockQuantity = x.StockQuantity
                }).ToListAsync();

            return products;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _repository.GetAll(x => x.Id == id)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    StockQuantity = x.StockQuantity
                }).FirstOrDefaultAsync();

            return product;
        }

        public async Task<ServiceMessage> UpdateProduct(UpdateProductDto product)
        {
            var productEntity = _repository.GetById(product.Id);

            if (productEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu id ile eşleşen ürün bulunamadı."
                };
            }

            productEntity.ProductName = product.ProductName;
            productEntity.Price = product.Price;
            productEntity.StockQuantity = product.StockQuantity;

            _repository.Update(productEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Güncelleme esnasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };

        }
    }
}
