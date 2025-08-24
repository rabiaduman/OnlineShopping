using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShopping.Data.Entities
{
    public class OrderProductEntity : BaseEntity
    {
        public int OrderId { get; set; } //Hangi siparişte

        public int ProductId { get; set; } //Hangi ürün

        public int Quantity { get; set; }

        //Relational Property

        public OrderEntity Order { get; set; }

        public ProductEntity Product { get; set; }

    }

    public class OrderProductConfiguration : BaseConfiguration<OrderProductEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderProductEntity> builder)
        {
            builder.Ignore(x => x.Id); //BaseEntity'den gelen Id propertysini görmezden geldik, tabloya aktarılmayacak.
            builder.HasKey("OrderId", "ProductId"); //Composite Key oluşturup yeni Primary Key olarak atadık.
            base.Configure(builder);
        }
    }
}
