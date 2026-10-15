var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1. Đăng ký dịch vụ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Port mặc định của Vite React
              .AllowAnyHeader()                     // Cho phép mọi Header (Authorization, Content-Type...)
              .AllowAnyMethod()                     // Cho phép mọi method (GET, POST, PUT, DELETE...)
              .AllowCredentials();                  // Cho phép gửi Cookie/Auth Header nếu cần
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// 2. Kích hoạt Middleware CORS (LƯU Ý: Đặt TRƯỚC UseAuthorization và MapControllers)
app.UseCors("AllowReactApp");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
