var builder = WebApplication.CreateBuilder(args);

// Khai báo Service và đăng ký vòng đời
builder.Services.AddTransient<ITransientService, OperationService>();
builder.Services.AddScoped<IScopedService, OperationService>();
builder.Services.AddSingleton<ISingletonService, OperationService>();

var app = builder.Build();
//chạy app, rồi lên web chạy http://localhost:5000/api/di-demo
app.MapGet("/api/di-demo", (
    ITransientService t1, ITransientService t2,
    IScopedService s1, IScopedService s2,
    ISingletonService sng1, ISingletonService sng2) =>
{
    return Results.Ok(new
    {
        Transient = new { Lan_1 = t1.Id.ToString()[..8], Lan_2 = t2.Id.ToString()[..8] },
        Scoped = new { Lan_1 = s1.Id.ToString()[..8], Lan_2 = s2.Id.ToString()[..8] },
        Singleton = new { Lan_1 = sng1.Id.ToString()[..8], Lan_2 = sng2.Id.ToString()[..8] }
    });
});

app.Run();

public interface ITransientService { Guid Id { get; } }
public interface IScopedService { Guid Id { get; } }
public interface ISingletonService { Guid Id { get; } }

public class OperationService : ITransientService, IScopedService, ISingletonService
{
    public Guid Id { get; } = Guid.NewGuid();
}