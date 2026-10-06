//Lambda Expression
// 1. Không dùng Lambda (Cách cổ điển dùng delegate)
//bool KiemTraSoChan(int x) { return x % 2 == 0; }

//// 2. Dùng Lambda Expression (Ngắn gọn, biểu cảm)
//Func<int, bool> kiemTra = x => x % 2 == 0;

//Console.WriteLine(kiemTra(4)); // In ra: True

//2. LINQ:
/*
 Demo 1: Bài toán: Lấy danh sách sản phẩm ngành hàng "Electronics" còn hàng trong kho (Stock > 0), 
sắp xếp giảm dần theo giá tiền, và chỉ trích xuất thông tin cần thiết trả về cho Client.
 */
using System;
using System.Collections.Generic;
using System.Linq;

//public class Program
//{
//    public static void Main()
//    {
//        List<Product> products =
//        [
//            new Product { Id = 1, Name = "Laptop Dell", Category = "Electronics", Price = 1500, Stock = 5 },
//            new Product { Id = 2, Name = "Chuột Logitech", Category = "Electronics", Price = 30, Stock = 0 },
//            new Product { Id = 3, Name = "Bàn phím cơ", Category = "Electronics", Price = 90, Stock = 12 },
//            new Product { Id = 4, Name = "Áo thun Polo", Category = "Fashion", Price = 25, Stock = 50 },
//            new Product { Id = 5, Name = "Quần Jeans", Category = "Fashion", Price = 45, Stock = 20 },
//            new Product { Id = 6, Name = "Màn hình LG 27 inch", Category = "Electronics", Price = 300, Stock = 7 }
//        ];

//        var result = products
//            .Where(p => p.Category == "Electronics" && p.Stock > 0)
//            .OrderByDescending(p => p.Price)
//            .Select(p => new
//            {
//                p.Id,
//                p.Name,
//                FormattedPrice = $"${p.Price:N0}"
//            })
//            .ToList();

//        Console.WriteLine("--- DANH SÁCH LINH KIỆN CÒN HÀNG (GIẢM DẦN THEO GIÁ) ---");
//        foreach (var item in result)
//        {
//            Console.WriteLine($"[ID: {item.Id}] {item.Name,-20} | Giá: {item.FormattedPrice}");
//        }
//    }
//}

/*
 Demo 2: Kiểm tra điều kiện (Any, All) và Lấy phần tử an toàn (FirstOrDefault)
Bài toán: Kiểm tra xem kho có bị hết hàng món nào không và tìm kiếm sản phẩm theo điều kiện linh hoạt 
mà không sợ crash chương trình.
 */
//public class Program
//{
//    public static void Main()
//    {
//        List<Product> products =
//        [
//            new Product { Id = 1, Name = "Laptop Dell", Category = "Electronics", Price = 1500, Stock = 5 },
//            new Product { Id = 2, Name = "Chuột Logitech", Category = "Electronics", Price = 30, Stock = 0 },
//            new Product { Id = 3, Name = "Bàn phím cơ", Category = "Electronics", Price = 90, Stock = 12 },
//            new Product { Id = 4, Name = "Áo thun Polo", Category = "Fashion", Price = 25, Stock = 50 },
//            new Product { Id = 5, Name = "Quần Jeans", Category = "Fashion", Price = 45, Stock = 20 },
//            new Product { Id = 6, Name = "Màn hình LG 27 inch", Category = "Electronics", Price = 300, Stock = 7 }
//        ];

//        // 1. Any: Kiểm tra có ít nhất 1 phần tử thỏa mãn điều kiện không (trả về bool)
//        bool coSanPhamHetHang = products.Any(p => p.Stock == 0);
//        Console.WriteLine($"Kho có sản phẩm nào hết hàng không? {coSanPhamHetHang}");

//        // 2. All: Kiểm tra TẤT CẢ phần tử có thỏa mãn không
//        bool tatCaCoGiaLonHonKhong = products.All(p => p.Price > 0);
//        Console.WriteLine($"Tất cả sản phẩm đều có giá hợp lệ? {tatCaCoGiaLonHonKhong}");

//        // 3. FirstOrDefault: Tìm sản phẩm đầu tiên thỏa mãn. 
//        // Nếu không tìm thấy, trả về null thay vì văng ngoại lệ InvalidOperationException như First().
//        Product spDatTien = products.FirstOrDefault(p => p.Price > 2000);

//        if (spDatTien != null)
//        {
//            Console.WriteLine($"Tìm thấy: {spDatTien.Name}");
//        }
//        else
//        {
//            Console.WriteLine("Không có sản phẩm nào vượt quá $2,000.");
//        }
//    }
//}

/*
 Demo 3: Gom nhóm (GroupBy) và Phân trang API (Skip & Take)
Bài toán: Thống kê số lượng hàng tồn kho theo từng ngành hàng, sau đó mô phỏng chức năng phân trang (Pagination) thường dùng trong Web API.
 */
public class Program
{
    public static void Main()
    {
        List<Product> products =
        [
            new Product { Id = 1, Name = "Laptop Dell", Category = "Electronics", Price = 1500, Stock = 5 },
            new Product { Id = 2, Name = "Chuột Logitech", Category = "Electronics", Price = 30, Stock = 0 },
            new Product { Id = 3, Name = "Bàn phím cơ", Category = "Electronics", Price = 90, Stock = 12 },
            new Product { Id = 4, Name = "Áo thun Polo", Category = "Fashion", Price = 25, Stock = 50 },
            new Product { Id = 5, Name = "Quần Jeans", Category = "Fashion", Price = 45, Stock = 20 },
            new Product { Id = 6, Name = "Màn hình LG 27 inch", Category = "Electronics", Price = 300, Stock = 7 }
        ];
        // 1. GroupBy: Gom nhóm sản phẩm theo Category và tính toán thống kê
        var thongKeTheoNganhHang = products
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                NganhHang = g.Key,
                TongSoLuongTon = g.Sum(p => p.Stock),
                GiaTrungBinh = g.Average(p => p.Price)
            });

        Console.WriteLine("--- THỐNG KÊ KHO HÀNG ---");
        foreach (var item in thongKeTheoNganhHang)
        {
            Console.WriteLine($"Ngành: {item.NganhHang,-12} | Tồn: {item.TongSoLuongTon,3} món | Giá TB: ${item.GiaTrungBinh:F1}");
        }

        // 2. Skip & Take: Kỹ thuật phân trang (Paging)
        int pageIndex = 2; // Người dùng xem Trang 2
        int pageSize = 2;  // Mỗi trang hiển thị 2 sản phẩm

        var danhSachTrang2 = products
            .OrderBy(p => p.Id) // Luôn sắp xếp trước khi phân trang
            .Skip((pageIndex - 1) * pageSize) // Bỏ qua các sản phẩm của Trang 1
            .Take(pageSize)                   // Lấy đúng số lượng của Trang 2
            .ToList();

        Console.WriteLine($"\n--- SẢN PHẨM TRANG {pageIndex} (PAGE SIZE: {pageSize}) ---");
        foreach (var sp in danhSachTrang2)
        {
            Console.WriteLine($"[ID: {sp.Id}] {sp.Name} - ${sp.Price}");
        }
    }

}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
