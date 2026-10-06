using Middleware_Demo; // 1. Import namespace chứa Middleware
using Microsoft.AspNetCore.Builder;
 var builder = WebApplication.CreateBuilder(args);

// 3. Đăng ký Controller service trước khi Build
builder.Services.AddControllers();

var app = builder.Build();

// Đặt middleware đo hiệu năng ở đầu pipeline
app.UseMiddleware<PerformanceMonitoringMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

