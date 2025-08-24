using OnlineShopping.Business.Operations.Setting;

namespace OnlineShopping.WebApi.Middlewares
{
    public class MaintenanceMiddleware
    {
        //Delegate'ı dependency injection ile çekiyorum
        private readonly RequestDelegate _next;

        public MaintenanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenanceMode = settingService.GetMaintenanceState(); // Bakım modunun açık olup olmadığını kontrol et

            // Eğer istek login veya setting endpointlerine yapılıyorsa, kontrol etmeden devam et
            if (context.Request.Path.StartsWithSegments("/api/auth/login") || context.Request.Path.StartsWithSegments("/api/settings"))
            {
                await _next(context); // bir sonraki middleware'e geç
                return;
            }

            if (maintenanceMode) // Eğer bakım modu aktifse, istekleri engelle ve mesaj göster
            {
                await context.Response.WriteAsync("Şu anda hizmet veremiyoruz.");
            }
            else // Bakım modu kapalıysa işlemlere normal şekilde devam et
            {
                await _next(context);
            }
        }
    }
}
