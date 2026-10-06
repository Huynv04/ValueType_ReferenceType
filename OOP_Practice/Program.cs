//Encapsulation
//using System;

//var tk = new TaiKhoanNganHang("Nguyễn Văn Huy", 1000);

//// Nạp tiền hợp lệ
//tk.NapTien(500);

//// Rút tiền vượt quá số dư hiện tại
//tk.RutTien(2000);

//// Xem số dư qua Property (chỉ đọc)
//Console.WriteLine($"Chủ tài khoản: {tk.ChuTaiKhoan} | Số dư cuối: {tk.SoDu:N0} VND");

//public class TaiKhoanNganHang
//{
//    public string ChuTaiKhoan { get; }

//    // Dữ liệu nhạy cảm được ẩn giấu hoàn toàn
//    private decimal _soDu;

//    // Chỉ cho phép đọc từ bên ngoài, không cho phép gán trực tiếp tk.SoDu = ...
//    public decimal SoDu => _soDu;

//    public TaiKhoanNganHang(string chuTaiKhoan, decimal soDuBanDau)
//    {
//        ChuTaiKhoan = chuTaiKhoan;
//        _soDu = soDuBanDau > 0 ? soDuBanDau : 0;
//    }

//    public void NapTien(decimal soTien)
//    {
//        if (soTien <= 0)
//        {
//            Console.WriteLine("[Lỗi]: Số tiền nạp phải lớn hơn 0!");
//            return;
//        }
//        _soDu += soTien;
//        Console.WriteLine($"[Thành công]: Nạp {soTien:N0} VND. Số dư mới: {_soDu:N0} VND");
//    }

//    public void RutTien(decimal soTien)
//    {
//        if (soTien > _soDu)
//        {
//            Console.WriteLine($"[Từ chối]: Số dư không đủ để rút {soTien:N0} VND (Hiện có: {_soDu:N0} VND)!");
//            return;
//        }
//        _soDu -= soTien;
//        Console.WriteLine($"[Thành công]: Rút {soTien:N0} VND. Số dư còn: {_soDu:N0} VND");
//    }
//}


//Inheritance
//using System;
//var dev = new LapTrinhVien("Huy", "NV-2026", "C# / .NET");
//dev.HienThiThongTin();

//public class NhanVien
//{
//    public string HoTen { get; set; }

//    // protected: Class con truy cập được, nhưng ngoài Program không gọi được dev._maSo
//    protected string _maSo;

//    public NhanVien(string hoTen, string maSo)
//    {
//        HoTen = hoTen;
//        _maSo = maSo;
//        Console.WriteLine("-> Constructor của NhanVien (Class cha) chạy.");
//    }
//}

//public class LapTrinhVien : NhanVien
//{
//    public string ChuyenMon { get; set; }

//    // Gọi constructor của cha bằng từ khóa 'base'
//    public LapTrinhVien(string hoTen, string maSo, string chuyenMon)
//        : base(hoTen, maSo)
//    {
//        ChuyenMon = chuyenMon;
//        Console.WriteLine("-> Constructor của LapTrinhVien (Class con) chạy.");
//    }

//    public void HienThiThongTin()
//    {
//        // Truy cập trực tiếp _maSo từ class cha
//        Console.WriteLine($"[Nhân viên]: {HoTen} | Mã số: {_maSo} | Tech stack: {ChuyenMon}");
//    }
//}

//Polymorphism
//using System;
//using System.Collections.Generic;

//// 1. MINH HỌA OVERLOADING (Compile-time)
//var mayTinh = new MayTinh();
//Console.WriteLine($"Cộng 2 số nguyên: {mayTinh.Cong(5, 10)}");
//Console.WriteLine($"Cộng 2 số thực:   {mayTinh.Cong(5.5, 2.3)}");

//Console.WriteLine("\n--- MINH HỌA OVERRIDING (Runtime) ---");

//// 2. MINH HỌA OVERRIDING (Runtime)
//// Danh sách chứa kiểu cha DongVat, nhưng các đối tượng thực tế trên Heap là Cho và Meo
//List<DongVat> danhSachDongVat = new List<DongVat>
//{
//    new Cho(),
//    new Meo(),
//    new DongVat()
//};

//foreach (var dv in danhSachDongVat)
//{
//    // Cùng gọi hàm PhatTiengKeu(), nhưng mỗi con phát ra tiếng khác nhau
//    dv.PhatTiengKeu();
//}

//public class MayTinh
//{
//    // 1. OVERLOADING: Cùng tên hàm, khác danh sách tham số, Cùng tên 'Cong', khác kiểu dữ liệu tham số
//    public int Cong(int a, int b) => a + b;
//    public double Cong(double a, double b) => a + b;
//}

//public class DongVat
//{
//    // virtual: Cho phép class con ghi đè
//    public virtual void PhatTiengKeu() => Console.WriteLine("Động vật phát ra âm thanh chung.");
//}

//public class Cho : DongVat
//{
//    // 2. OVERRIDING: Ghi đè hành vi lúc Runtime
//    public override void PhatTiengKeu() => Console.WriteLine("Chó sủa: Gâu gâu!");
//}

//public class Meo : DongVat
//{
//    //2. OVERRIDING: Ghi đè hành vi lúc Runtime
//    public override void PhatTiengKeu() => Console.WriteLine("Mèo kêu: Meo meo!");
//}

//Abstraction
using System;

// Khởi tạo đối tượng qua interface và abstract class
DichVuThongBao thongBao = new ThongBaoEmail();
thongBao.Gui("client@example.com", "Hóa đơn khám bệnh của bạn đã sẵn sàng.");

ICongThanhToan thanhToan = new ThongBaoEmail();
thanhToan.XuLyGiaoDich(250000);

// INTERFACE: Hợp đồng nghiệp vụ (CAN-DO)
public interface ICongThanhToan
{
    void XuLyGiaoDich(decimal soTien);
}

// ABSTRACT CLASS: Lớp nền tảng (IS-A)
public abstract class DichVuThongBao
{
    // Phương thức có sẵn code để tái sử dụng
    public void GhiLogHeThong(string noiDung)
    {
        Console.WriteLine($"[Hệ thống Log]: {noiDung}");
    }

    // Phương thức bắt buộc class con phải tự triển khai chi tiết
    public abstract void Gui(string nguoiNhan, string noiDung);
}

// Class vừa kế thừa Abstract Class vừa triển khai Interface
public class ThongBaoEmail : DichVuThongBao, ICongThanhToan
{
    public override void Gui(string nguoiNhan, string noiDung)
    {
        GhiLogHeThong($"Chuẩn bị gửi mail tới {nguoiNhan}");
        Console.WriteLine($"-> [Email Sent]: Gửi tới {nguoiNhan} - Nội dung: '{noiDung}'");
    }

    public void XuLyGiaoDich(decimal soTien)
    {
        Console.WriteLine($"-> [Thanh toán]: Trừ tài khoản {soTien:N0} VND qua cổng trực tuyến thành công.");
    }
}