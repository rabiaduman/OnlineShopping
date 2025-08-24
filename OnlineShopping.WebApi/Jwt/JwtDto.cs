using OnlineShopping.Data.Enums;

namespace OnlineShopping.WebApi.Jwt
{
    public class JwtDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType Role { get; set; }

        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpireMinutes { get; set; }


    }
}
