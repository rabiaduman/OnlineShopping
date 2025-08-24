using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Business.Operations.Order.Dtos;
using OnlineShopping.Business.Types;
using OnlineShopping.Data.Entities;
using OnlineShopping.Data.Repositories;
using OnlineShopping.Data.UnitOfWork;

namespace OnlineShopping.Business.Operations.Order
{
    public class OrderManager : IOrderService
    {
        // Gerekli bağımlılıklar
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderProductEntity> _orderProductRepository;
        private readonly IRepository<ProductEntity> _productRepository;

        // Bağımlılıkların constructor aracılığıyla enjekte edilmesi (Dependency Injection)
        public OrderManager(IUnitOfWork unitOfWork, IRepository<OrderEntity> orderRepository, IRepository<OrderProductEntity> orderProductRepository, IRepository<ProductEntity> productRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _productRepository = productRepository;
        }

        public async Task<ServiceMessage> AddOrder(AddOrderDto order) // Sipariş ekleme
        {
            decimal totalAmount = 0;

            foreach (var item in order.Products)  // Her ürün için kontrol ve toplam fiyat hesaplama
            { 
                var product = _productRepository.GetById(item.ProductId); // Ürünü veritabanından getir

                if (product is null)
                {
                    return new ServiceMessage
                    {
                        IsSucceed = false,
                        Message = "Ürün bulunamadı."
                    };
                }
                
                if (product.StockQuantity < item.Quantity) // Stok kontrolü
                {
                    return new ServiceMessage
                    {
                        IsSucceed = false,
                        Message = "Yeterli sayıda ürün yok."
                    };
                }
                
                totalAmount += product.Price * item.Quantity; // Fiyat hesaplama
            }

            await _unitOfWork.BeginTransaction(); // Transaction başlatılır

            try
            {
                var orderEntity = new OrderEntity // Sipariş oluşturulur
                {
                    UserId = order.UserId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                };

                _orderRepository.Add(orderEntity); // Sipariş veritabanına eklenir

                await _unitOfWork.SaveChangesAsync(); // Sipariş verisi kaydedilir

                foreach (var item in order.Products)
                {
                    // Siparişteki ürünler işlenir
                    var orderProduct = new OrderProductEntity
                    {
                        OrderId = orderEntity.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };

                    _orderProductRepository.Add(orderProduct);

                    // Ürünün stok miktarı düşürülür
                    var finalProduct = _productRepository.GetById(item.ProductId); 
                    finalProduct.StockQuantity -= item.Quantity;

                    _productRepository.Update(finalProduct); // Ürün güncellenir
                }

                await _unitOfWork.SaveChangesAsync(); // Tüm değişiklikler kaydedilir
                await _unitOfWork.CommitTransaction(); // Transaction tamamlanır
            }
            catch
            {
                await _unitOfWork.RollBackTransaction(); // Hata durumunda geri alınır

                throw;
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Sipariş oluşturuldu."
            };

        }

        public async Task<ServiceMessage> DeleteOrder(int id)  // Siparişi silme işlemini gerçekleştirir
        {
            var order = _orderRepository.GetById(id);

            if (order is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Sipariş bulunamadı."
                };
            }
            // Siparişteki ürünler alınır
            var orderProducts = _orderProductRepository.GetAll(x => x.OrderId == id)
                .Include(x => x.Product)
                .ToList();

            if (!orderProducts.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Sipariş ürünleri bulunamadı."
                };
            }

            await _unitOfWork.BeginTransaction();

            try
            {
                foreach (var item in orderProducts)
                {
                    // Stok iadesi yapılır
                    item.Product.StockQuantity += item.Quantity;
                    _productRepository.Update(item.Product);
                }
                // Sipariş silinir
                _orderRepository.Delete(id); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollBackTransaction();
                throw;
            }
         
            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Sipariş silindi."
            };
        }

        public async Task<OrderDto> GetOrderById(int id) // Sipariş ID'sine göre siparişi getirir
        {
            var order = await _orderRepository.GetAll(x => x.Id == id)
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    TotalAmount = x.TotalAmount,
                    Products = x.OrderProducts.Select(p => new OrderProductsDto
                    {
                        Id = p.ProductId,
                        ProductName = p.Product.ProductName,
                        Quantity = p.Quantity
                    }).ToList()
                }).FirstOrDefaultAsync();

            return order;
        }

        public async Task<List<OrderDto>> GetOrders() // Tüm siparişleri getirir
        {
            
            var orders = await _orderRepository.GetAll()
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    TotalAmount = x.TotalAmount,
                    Products = x.OrderProducts.Select(p => new OrderProductsDto
                    {
                        Id = p.ProductId,
                        ProductName = p.Product.ProductName,
                        Quantity = p.Quantity
                    }).ToList()
                }).ToListAsync();

            return orders;
        }
    }
}
