using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopping.Data.Enums;

namespace OnlineShopping.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; } //Giriş işlemlerinde kullanılacak

        public string Password { get; set; } //Giriş işlemlerinde kullanılacak

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public UserType Role { get; set; } //Kullanıcı rolü admin veya müşteri gibi yetkilendirme işlemleri için.

        //Relational Property

        public ICollection<OrderEntity> Orders { get; set; }
       
    }

    public class UserConfiguration : BaseConfiguration<UserEntity> 
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(35);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(40);
            builder.Property(x => x.Email)
                .IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            base.Configure(builder);
        }
    }


}
