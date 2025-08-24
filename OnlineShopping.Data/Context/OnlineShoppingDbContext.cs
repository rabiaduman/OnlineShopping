using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.Entities;

namespace OnlineShopping.Data.Context
{
    public class OnlineShoppingDbContext : DbContext
    {
        public OnlineShoppingDbContext(DbContextOptions<OnlineShoppingDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Api
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenenceMode = false
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<ProductEntity> Products => Set<ProductEntity>();

        public DbSet<OrderEntity> Orders => Set<OrderEntity>();

        public DbSet<OrderProductEntity> OrderProducts => Set<OrderProductEntity>();

        public DbSet<SettingEntity> Settings {  get; set; }
    }
}
