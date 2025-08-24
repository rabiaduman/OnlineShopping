using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineShopping.WebApi.Filters
{
    public class TimeControlFilter : ActionFilterAttribute
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context) // Action çalıştırılmadan hemen önce devreye girer
        {
            var now = DateTime.Now.TimeOfDay;

            // Erişime izin verilen saat aralığı (sabit olarak belirledim)
            StartTime = "23:30";
            EndTime = "23:59";

            if (now >= TimeSpan.Parse(StartTime) && now <= TimeSpan.Parse(EndTime)) // Eğer istek, izin verilen saat aralığındaysa işlemi devam ettir
            {
                base.OnActionExecuting(context); // İşleme devam et
            }
            else
            {
                context.Result = new ContentResult // Saat aralığı dışında ise 403 hatası döndür
                {
                    Content = "Bu saatler arasında bu end-pointe istek atılamaz.",
                    StatusCode = 403
                };
            }    
        }
    }
}
