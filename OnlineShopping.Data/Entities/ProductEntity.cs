using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShopping.Data.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        //Relational Property
        public ICollection<OrderProductEntity> OrderProducts { get; set; }
    }

    public class ProductConfiguration : BaseConfiguration<ProductEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(x => x.StockQuantity)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
