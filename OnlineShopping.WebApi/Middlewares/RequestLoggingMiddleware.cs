using OnlineShopping.WebApi.Jwt;

namespace OnlineShopping.WebApi.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<RequestLoggingMiddleware> _logger; // Loglama yapmak için kullanılan ILogger instance'ı

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)  //Dependency injection ile next delegate ve logger alınır
        {
            _next = next; // pipeline'da bir sonraki middleware
            _logger = logger; // loglama için kullanılan nesne
        }

        public async Task InvokeAsync(HttpContext context) // Middleware'in çağrıldığı asenkron method
        {           
            var url = context.Request.Path; // İstek yapılan URL'nin yolunu alıyoruz (örn: /api/products)
    
            var requestTime = DateTime.Now; // İstek anının zaman bilgisini alıyoruz
            
            string userId = "Anonymous"; // Varsayılan kullanıcı Id'si, eğer kullanıcı doğrulanmamışsa "Anonymous" olacak
            
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated) // Eğer kullanıcı kimliği varsa ve doğrulanmışsa, JWT içerisinden kullanıcı Id'sini alıyoruz
            {
                userId = context.User.FindFirst(JwtClaimNames.Id)?.Value ?? "UnknownUser"; // JWT'den Id claim'i çekiliyor
            }

            // Loglama yapıyoruz: istek zamanı, url ve kullanıcı Id'si bilgileri
            _logger.LogInformation("[REQUEST] Time: {RequestTime}, URL: {Url}, UserId: {UserId}",
                requestTime, url, userId);

            // Pipeline'daki bir sonraki middleware'i çalıştırıyoruz
            await _next(context);
        }
    }

}
