/*
 Demo: Thiết lập đầy đủ 3 quan hệ (1-1, 1-N, N-N) bằng Fluent API
Bài toán mô phỏng hệ thống quản lý bệnh viện/phòng khám:
1 - 1: Mỗi BacSi (Bác sĩ) có duy nhất 1 HoSoChiTiet (Hồ sơ lý lịch chi tiết).
1 - N: Một KhoaKham (Chuyên khoa) có nhiều BacSi.
N - N: Một BacSi có thể khám nhiều BenhNhan, và một BenhNhan có thể khám nhiều BacSi qua bảng trung gian LichKham.
 */
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

// ==========================================
// 1. CÁC ENTITY MODEL
// ==========================================
public class KhoaKham
{
    public int Id { get; set; }
    public string TenKhoa { get; set; }

    // Navigation property: 1 Khoa có nhiều Bác sĩ
    public ICollection<BacSi> DanhSachBacSi { get; set; } = new List<BacSi>();
}

public class HoSoChiTiet
{
    public int Id { get; set; }
    public string TieuSu { get; set; }
    public int SoNamKinhNghiem { get; set; }

    // Khóa ngoại trỏ về BacSi (Quan hệ 1 - 1)
    public int BacSiId { get; set; }
    public BacSi BacSi { get; set; }
}

public class BacSi
{
    public int Id { get; set; }
    public string HoTen { get; set; }

    // Quan hệ 1 - N: Thuộc về 1 Khoa
    public int KhoaKhamId { get; set; }
    public KhoaKham KhoaKham { get; set; }

    // Quan hệ 1 - 1: Có 1 hồ sơ
    public HoSoChiTiet HoSoChiTiet { get; set; }

    // Quan hệ N - N: Liên kết tới BenhNhan qua bảng trung gian LichKham
    public ICollection<LichKham> DanhSachLichKham { get; set; } = new List<LichKham>();
}

public class BenhNhan
{
    public int Id { get; set; }
    public string HoTen { get; set; }
    public string SoDienThoai { get; set; }

    public ICollection<LichKham> DanhSachLichKham { get; set; } = new List<LichKham>();
}

// Bảng trung gian (Payload Table / Join Entity)
public class LichKham
{
    public int Id { get; set; }
    public DateTime NgayKham { get; set; }
    public string ChanDoan { get; set; }

    public int BacSiId { get; set; }
    public BacSi BacSi { get; set; }

    public int BenhNhanId { get; set; }
    public BenhNhan BenhNhan { get; set; }
}

// ==========================================
// 2. CẤU HÌNH FLUENT API TRONG DBCONTEXT
// ==========================================
public class PhongKhamDbContext : DbContext
{
    public DbSet<KhoaKham> KhoaKhams { get; set; }
    public DbSet<BacSi> BacSis { get; set; }
    public DbSet<HoSoChiTiet> HoSoChiTiets { get; set; }
    public DbSet<BenhNhan> BenhNhans { get; set; }
    public DbSet<LichKham> LichKhams { get; set; }

    public PhongKhamDbContext(DbContextOptions<PhongKhamDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Cấu hình 1 - N: KhoaKham -> BacSi ---
        modelBuilder.Entity<BacSi>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.HoTen).IsRequired().HasMaxLength(150);

            entity.HasOne(bs => bs.KhoaKham)
                  .WithMany(kk => kk.DanhSachBacSi)
                  .HasForeignKey(bs => bs.KhoaKhamId)
                  .OnDelete(DeleteBehavior.Restrict); // Tránh xóa cascade làm mất bác sĩ khi xóa khoa
        });

        // --- Cấu hình 1 - 1: BacSi <-> HoSoChiTiet ---
        modelBuilder.Entity<HoSoChiTiet>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(hs => hs.BacSi)
                  .WithOne(bs => bs.HoSoChiTiet)
                  .HasForeignKey<HoSoChiTiet>(hs => hs.BacSiId)
                  .OnDelete(DeleteBehavior.Cascade); // Xóa bác sĩ thì xóa luôn hồ sơ chi tiết
        });

        // --- Cấu hình N - N (có thuộc tính bổ sung) qua LichKham ---
        modelBuilder.Entity<LichKham>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(lk => lk.BacSi)
                  .WithMany(bs => bs.DanhSachLichKham)
                  .HasForeignKey(lk => lk.BacSiId);

            entity.HasOne(lk => lk.BenhNhan)
                  .WithMany(bn => bn.DanhSachLichKham)
                  .HasForeignKey(lk => lk.BenhNhanId);
        });
    }
}

/*
 
 // ❌ RẤT TỆ (IEnumerable): SELECT toàn bộ 500.000 dòng về RAM rồi mới lọc
IEnumerable<BacSi> dsBacSi = _context.BacSis; 
var ketQua = dsBacSi.Where(x => x.HoTen.Contains("Huy")).Take(5).ToList();
// SQL sinh ra: SELECT [Id], [HoTen], [KhoaKhamId] FROM [BacSis] (Tải hết về RAM!)

// ✔️ CHUẨN DOANH NGHIỆP (IQueryable): Lọc trực tiếp dưới SQL Server
IQueryable<BacSi> query = _context.BacSis;
var ketQuaToiUu = query.Where(x => x.HoTen.Contains("Huy")).Take(5).ToList();
// SQL sinh ra: SELECT TOP(5) [Id], [HoTen], [KhoaKhamId] FROM [BacSis] WHERE [HoTen] LIKE '%Huy%'
 

//B. AsNoTracking() – Tắt bộ theo dõi trạng thái
 public async Task<List<BacSi>> GetAllDoctorsReadOnlyAsync()
{
    return await _context.BacSis
        .AsNoTracking() // EF Core bỏ qua bước Change Tracker, giải phóng bộ nhớ ngay lập tức
        .ToListAsync();
}
 
 
 
 */