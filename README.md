# 🛒 OnlineShopping Web API

OnlineShopping, ASP.NET Core ile geliştirilmiş bir e-ticaret uygulamasıdır. Bu projede kullanıcı yönetimi, ürün ve sipariş işlemleri gibi temel e-ticaret fonksiyonları yer almaktadır. Ayrıca, güvenlik, logging, global hata yönetimi gibi birçok gelişmiş özellik de uygulanmıştır.

---

## 🚀 Teknolojiler ve Araçlar

- ASP.NET Core 7.0
- Entity Framework Core
- SQL Server
- JWT (JSON Web Token)
- Swagger / Swashbuckle
- AutoMapper
- Middleware
- Action Filters
- Data Protection
- Dependency Injection
- LINQ & Async/Await
- Git / GitHub

---

## 🧩 Katmanlar

Proje en az 3 katmandan oluşmaktadır:

- **OnlineShopping.Data** → Entity'ler, DbContext, Repository, UnitOfWork
- **OnlineShopping.Business** → Servisler, DTO’lar, Business Logic
- **OnlineShopping.WebApi** → Controller’lar, Middleware’ler, JWT ve HTTP konfigürasyonları

---

## 📚 Proje Özellikleri

✅ **3 katmanlı mimari**  
✅ **Çoka-çok ilişki** (Sipariş & Ürün)  
✅ **Entity Framework Code First**  
✅ **CRUD (Get, Post, Put, Patch, Delete)**  
✅ **Authentication & Authorization (JWT ile)**  
✅ **Custom kullanıcı yönetimi**  
✅ **Middleware (Exception Handling, Maintenance, Logging)**  
✅ **Action Filter** (Zaman kontrolü)  
✅ **Model Validation**  
✅ **Dependency Injection**  
✅ **Data Protection (Şifreleme)**  
✅ **Global Exception Handling**  
✅ **Swagger UI ile test**  

---

## 🔐 Kimlik Doğrulama ve Yetkilendirme

- Login işlemi sonrası JWT token döner.
- Swagger üzerinden "Authorize" butonuna tıklayıp `Bearer <token>` şeklinde token'ı girerek tüm yetkili endpoint’ler test edilebilir.
- Her kullanıcı rol bazlı yetkilendirilir.

---

## ⚙️ Swagger Kullanımı

1. Projeyi çalıştırın.
2. Swagger arayüzü otomatik açılır.
3. `/api/Auth/login` endpoint’i ile giriş yapın ve JWT token alın.
4. Sağ üstteki **Authorize** butonuna tıklayın.
5. `Bearer <token>` şeklinde token'ı yapıştırın ve "Authorize" deyin.
6. Diğer endpoint’leri token ile test edebilirsiniz.

---

## 🧪 Örnek Kullanım

```json
POST /api/Auth/login

{
  "email": "admin@onlineshopping.com",
  "password": "123456"
}
