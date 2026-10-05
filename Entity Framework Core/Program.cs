using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } // "Pending", "Completed", "Cancelled"
    public DateTime CreatedAt { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
public class OrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    // =========================================================================
    // 1. TRUY VẤN ENTITY (An toàn chống SQL Injection với FromSqlInterpolated)
    // =========================================================================
    public async Task<List<Order>> GetHighValueOrdersAsync(string status, decimal minAmount)
    {
        // EF Core sẽ tự động chuyển các biến nội suy {status} và {minAmount} 
        // thành SqlParamater (@p0, @p1), tuyệt đối KHÔNG bị dính lỗi SQL Injection.
        var orders = await _context.Orders
            .FromSqlInterpolated($@"
                SELECT * 
                FROM Orders 
                WHERE Status = {status} AND TotalAmount >= {minAmount}
                ORDER BY CreatedAt DESC")
            .AsNoTracking() // Dùng AsNoTracking nếu chỉ đọc để tiết kiệm RAM
            .ToListAsync();

        return orders;
    }

    // =========================================================================
    // 2. TRUY VẤN RA DTO TỰ DO KHÔNG CẦN TẠO DBSET (EF Core 8/9: Database.SqlQuery)
    // =========================================================================
    public class MonthlySalesSummaryDto
    {
        public int Month { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public async Task<List<MonthlySalesSummaryDto>> GetMonthlyRevenueReportAsync(int year)
    {
        // Khi cần JOIN phức tạp hoặc GROUP BY để báo cáo, 
        // dùng Database.SqlQuery<T> để map thẳng vào DTO mà không cần cấu hình DbContext.
        var report = await _context.Database
            .SqlQuery<MonthlySalesSummaryDto>($@"
                SELECT 
                    MONTH(CreatedAt) AS Month,
                    COUNT(Id) AS TotalOrders,
                    SUM(TotalAmount) AS TotalRevenue
                FROM Orders
                WHERE YEAR(CreatedAt) = {year} AND Status = 'Completed'
                GROUP BY MONTH(CreatedAt)
                ORDER BY Month ASC")
            .ToListAsync();

        return report;
    }

    // =========================================================================
    // 3. THỰC THI DML HÀNG LOẠT (UPDATE / DELETE không cần load vào bộ nhớ)
    // =========================================================================
    public async Task<int> CancelExpiredOrdersAsync(DateTime cutoffDate)
    {
        // Nếu dùng LINQ thông thường: phải SELECT toàn bộ Order lên Heap -> duyệt foreach -> SaveChanges()
        // Cách đó cực kỳ chậm nếu có 10.000 records.
        // Dùng ExecuteSqlInterpolatedAsync: bắn 1 câu UPDATE duy nhất xuống Database, xử lý tức thì.
        int rowsAffected = await _context.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE Orders 
            SET Status = 'Cancelled' 
            WHERE Status = 'Pending' AND CreatedAt < {cutoffDate}");

        return rowsAffected; // Trả về số dòng đã được update
    }
}