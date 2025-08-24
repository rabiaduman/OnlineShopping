using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShopping.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; } //? ile zorunlu tutmuyorum, veri eklendiğinde null olabilir

        public bool IsDeleted { get; set; } //Soft delete işlemleri için
    }

    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //Bu veritabanı üzerinde yapılacak bütün sorgulamalarda ve diğer linq işlemlerinde geçerli olacak bir filtreleme
            builder.HasQueryFilter(x => x.IsDeleted == false); 
            builder.Property(x => x.ModifiedDate)
                .IsRequired(false);

        }
    }
}
