var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware 1: Đo thời gian xử lý (Logging & Profiler)
app.Use(async (context, next) =>
{
    Console.WriteLine("[Middleware 1] -> BẮT ĐẦU: Nhận request Inbound");
    var watch = System.Diagnostics.Stopwatch.StartNew();

    await next(); // Chuyển quyền cho Middleware tiếp theo

    watch.Stop();
    Console.WriteLine($"[Middleware 1] <- KẾT THÚC: Outbound trả về Client (Thời gian: {watch.ElapsedMilliseconds} ms)");
});

// Middleware 2: Kiểm tra bảo mật (Authentication Inspector)
app.Use(async (context, next) =>
{
    Console.WriteLine("  [Middleware 2] -> BẮT ĐẦU: Kiểm tra API Key");

    // Giả lập kiểm tra hợp lệ và chuyển tiếp
    await next();

    Console.WriteLine("  [Middleware 2] <- KẾT THÚC: Bổ sung Header bảo mật vào Response");
    context.Response.Headers.Append("X-Server-Processed-By", ".NET-9-Engine");
});

// Endpoint xử lý nghiệp vụ chính
app.MapGet("/api/orders", () =>
{
    Console.WriteLine("    [Endpoint Core] ==> Đang truy vấn Database và tạo dữ liệu đơn hàng...");
    return Results.Ok(new { Message = "Lấy dữ liệu đơn hàng thành công" });
});

app.Run();