using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Middleware_Demo
{
    public class PerformanceMonitoringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMonitoringMiddleware> _logger;

        public PerformanceMonitoringMiddleware(RequestDelegate next, ILogger<PerformanceMonitoringMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // [CHIỀU VÀO]: Ghi nhận thời gian bắt đầu
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // Chuyển request cho Middleware tiếp theo trong pipeline
            await _next(context);

            // [CHIỀU RA]: Khi Controller đã xử lý xong và response quay ngược lại
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation(
                "HTTP {Method} {Path} thực thi trong {Elapsed} ms - Status: {StatusCode}",
                context.Request.Method,
                context.Request.Path,
                elapsedMilliseconds,
                context.Response.StatusCode);
        }
    }
}
