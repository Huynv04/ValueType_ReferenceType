//1. List<T> – Mảng động tuần tự (Dynamic Array)
using System;
//using System.Collections.Generic;

//List<string> danhSachLop = new List<string> { "Huy", "An", "Bình","Khang","Quang","Hung Minh"};

//// 1. Thêm phần tử
//danhSachLop.Add("Cường");
//danhSachLop.Insert(1, "Dung"); // Chèn vào vị trí index = 1

//// 2. Truy xuất theo index
//Console.WriteLine($"Phần tử tại index 0: {danhSachLop[0]}");

//// 3. Xóa phần tử
//danhSachLop.Remove("An"); // Xóa theo giá trị

//Console.WriteLine($"Số lượng hiện tại: {danhSachLop.Count}");
//Console.WriteLine("Danh sách:");
//foreach (var ten in danhSachLop)
//{
//    Console.Write($"{ten} ");
//}


//Dictionary<TKey, TValue> – Bảng băm cặp Khóa - Giá trị (Hash Table)
//using System;
//using System.Collections.Generic;

//Dictionary<string, decimal> bangGiaDichVu = new Dictionary<string, decimal>
//{
//    { "KhamTongQuat", 150000 },
//    { "XetNghiemMau", 200000 }
//};

//// 1. Thêm phần tử an toàn
//bangGiaDichVu.TryAdd("SieuAm", 300000);

//// 2. Cập nhật giá trị
//bangGiaDichVu["KhamTongQuat"] = 180000;

//// 3. Tra cứu an toàn bằng TryGetValue (tránh crash khi key không tồn tại)
//string maDV = "XetNghiemMau";
//if (bangGiaDichVu.TryGetValue(maDV, out decimal giaTien))
//{
//    Console.WriteLine($"Giá dịch vụ '{maDV}': {giaTien:N0} VND");
//}

//// 4. Kiểm tra tồn tại
//bool coSieuAm = bangGiaDichVu.ContainsKey("SieuAm");
//Console.WriteLine($"Có dịch vụ Siêu âm không? {coSieuAm}");



//3. HashSet<T> – Tập hợp phần tử không trùng lặp (Hash Set)
//using System;
//using System.Collections.Generic;

//HashSet<string> tags = new HashSet<string> { "csharp", "dotnet", "backend" };

//// 1. Thêm phần tử trùng lặp
//bool themThanhCong = tags.Add("csharp"); // Trả về false vì đã tồn tại
//Console.WriteLine($"Thêm 'csharp' lần 2 thành công? {themThanhCong}");

//// 2. Phép toán tập hợp
//HashSet<int> tapA = new HashSet<int> { 1, 2, 3, 4 };
//HashSet<int> tapB = new HashSet<int> { 3, 4, 5, 6 };

//// Lấy giao của 2 tập hợp (các phần tử chung)
//tapA.IntersectWith(tapB);

//Console.Write("Giao của tập A và B: ");
//foreach (var item in tapA)
//{
//    Console.Write($"{item} ");
//}

// 4. Queue<T> – Hàng đợi FIFO (First In, First Out)
//using System;
//using System.Collections.Generic;

//Queue<string> hangDoiKham = new Queue<string>();

//// Bệnh nhân lấy số thứ tự
//hangDoiKham.Enqueue("Bệnh nhân A");
//hangDoiKham.Enqueue("Bệnh nhân B");
//hangDoiKham.Enqueue("Bệnh nhân C");
//hangDoiKham.Enqueue("Bệnh nhân D");
//hangDoiKham.Enqueue("Bệnh nhân E");
//hangDoiKham.Enqueue("Bệnh nhân F");


//Console.WriteLine($"Người tiếp theo được khám: {hangDoiKham.Peek()}");

//// Bác sĩ gọi vào khám lần lượt
//while (hangDoiKham.Count > 0)
//{
//    string bn = hangDoiKham.Dequeue(); // Lấy ra khỏi hàng đợi
//    Console.WriteLine($"Đang khám cho: {bn} (Còn lại trong hàng: {hangDoiKham.Count})");
//}

//5. Stack<T> – Ngăn xếp LIFO (Last In, First Out)
//using System;
//using System.Collections.Generic;

//Stack<string> lichSuTrang = new Stack<string>();

//// Người dùng duyệt web
//lichSuTrang.Push("TrangChu.html");
//lichSuTrang.Push("DanhSachBacSi.html");
//lichSuTrang.Push("DatLichKham.html");
//lichSuTrang.Push("SoKhamBenh.html");


//Console.WriteLine($"Trang hiện tại: {lichSuTrang.Peek()}");

//// Nhấn nút Back (Quay lại)
//string quayLai1 = lichSuTrang.Pop();
//Console.WriteLine($"Rời khỏi: {quayLai1} -> Quay về: {lichSuTrang.Peek()}");

//string quayLai2 = lichSuTrang.Pop();
//Console.WriteLine($"Rời khỏi: {quayLai2} -> Quay về: {lichSuTrang.Peek()}");


using System;
class Program
{
    static void Main()
    {
        // 1. Áp dụng cho kiểu int (Value Type) - Không bị Boxing!
        int x = 10, y = 20;
        Console.WriteLine($"Trước hoán đổi: x = {x}, y = {y}");
        HoanDoi(ref x, ref y);
        Console.WriteLine($"Sau hoán đổi:   x = {x}, y = {y}");

        // 2. Áp dụng cho kiểu string (Reference Type)
        string str1 = "Backend", str2 = "Frontend";
        Console.WriteLine($"\nTrước hoán đổi: str1 = {str1}, str2 = {str2}");
        HoanDoi(ref str1, ref str2);
        Console.WriteLine($"Sau hoán đổi:   str1 = {str1}, str2 = {str2}");

        // 3. Hàm generic in thông tin kiểm tra kiểu
        Console.WriteLine("\n--- Kiểm tra kiểu dữ liệu ---");
        InThongTinKieu(2026);
        InThongTinKieu("Nguyễn Văn Huy");
        InThongTinKieu(true);
    }

    // Generic Method hoán đổi giá trị 2 biến
    static void HoanDoi<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    // Generic Method in giá trị kèm kiểu dữ liệu của T
    static void InThongTinKieu<T>(T giaTri)
    {
        Console.WriteLine($"Giá trị: {giaTri,-16} | Kiểu thực tế: {typeof(T).FullName}");
    }
}