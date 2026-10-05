/*
 List<T> – Danh sách động và cơ chế Count vs Capacity
 */
List<int> numbers = new List<int>();

Console.WriteLine($"Ban đầu - Count: {numbers.Count}, Capacity: {numbers.Capacity}"); // 0, 0

// Thêm phần tử
numbers.Add(10);
numbers.Add(20);
numbers.AddRange(new int[] { 30, 40, 50 });

// Duyệt và xóa
numbers.Remove(20);         // Xóa theo giá trị
numbers.RemoveAt(0);        // Xóa phần tử đầu tiên theo index

Console.WriteLine($"Sau khi chỉnh sửa - Count: {numbers.Count}, Capacity: {numbers.Capacity}");
// Count là số phần tử thực tế (3), Capacity là dung lượng mảng đệm bên dưới (thường là 8)

foreach (var item in numbers)
{
    Console.Write($"{item} "); // In ra: 30 40 50
}

/*
 Dictionary<TKey, TValue> – Tra cứu siêu tốc $O(1)$ và cách dùng 
 */
// Quản lý thông tin nhân viên theo mã định danh (Id)
Dictionary<string, string> users = new Dictionary<string, string>
{
    { "NV01", "Nguyễn Văn A" },
    { "NV02", "Trần Thị B" }
};

// Thêm phần tử an toàn
users.TryAdd("NV03", "Lê Văn C");

// ❌ Cách KHÔNG NÊN làm (gây crash nếu không tìm thấy key):
// string name = users["NV99"]; // Văng KeyNotFoundException!

// ✔️ Cách chuẩn doanh nghiệp: Dùng TryGetValue
string searchId = "NV02";
if (users.TryGetValue(searchId, out string foundName))
{
    Console.WriteLine($"Tìm thấy: {foundName}");
}
else
{
    Console.WriteLine($"Không tìm thấy mã {searchId}");
}

/*
 HashSet<T>
 */
HashSet<string> registeredEmails = new HashSet<string>
{
    "user1@gmail.com",
    "user2@gmail.com"
};

// Thêm trùng lặp -> Add trả về false, không báo lỗi
bool isAdded = registeredEmails.Add("user1@gmail.com");
Console.WriteLine($"Thêm email trùng thành công? {isAdded}"); // False

// Phép toán tập hợp
HashSet<int> setA = new HashSet<int> { 1, 2, 3, 4 };
HashSet<int> setB = new HashSet<int> { 3, 4, 5, 6 };

setA.IntersectWith(setB); // Lấy giao giữa 2 tập hợp (các phần tử chung)

Console.WriteLine("Các phần tử chung:");
foreach (var num in setA)
{
    Console.Write($"{num} "); // In ra: 3 4
}