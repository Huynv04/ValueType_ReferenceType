/*
 Generic Method – Viết một lần, dùng cho mọi kiểu dữ liệu
 */
public class Utility
{
    // Hàm Generic hoán đổi giá trị của 2 biến thuộc bất kỳ kiểu dữ liệu nào
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}

// 1. Áp dụng cho int (Value Type)
int x = 5, y = 10;
Utility.Swap<int>(ref x, ref y);
Console.WriteLine($"x = {x}, y = {y}"); // x = 10, y = 5

// 2. Áp dụng cho string (Reference Type)
string str1 = "Hello", str2 = "World";
Utility.Swap(ref str1, ref str2); // C# có thể tự suy luận kiểu <string>
Console.WriteLine($"{str1} {str2}"); // World Hello


//Generic Class – Chuẩn hóa phản hồi API (ApiResponse<T>)
// Chuẩn hóa response API: data có thể là User, List<Product>, Order,...
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Thành công")
    {
        return new ApiResponse<T> { Success = true, Message = message, Data = data };
    }

    public static ApiResponse<T> Fail(string message)
    {
        return new ApiResponse<T> { Success = false, Message = message, Data = default };
    }
}

// Sử dụng trong Controller
var userResponse = ApiResponse<string>.Ok("Nguyễn Văn A", "Lấy user thành công");
var countResponse = ApiResponse<int>.Ok(100);

/// Generic Constraints
public interface IEntity
{
    int Id { get; set; }
}

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// Ràng buộc: T bắt buộc phải kế thừa IEntity và phải có constructor mặc định (new())
public class BaseRepository<T> where T : class, IEntity, new()
{
    public void PrintEntityId(T entity)
    {
        // Nhờ ràng buộc IEntity, trình biên dịch cho phép truy cập trực tiếp vào thuộc tính .Id
        Console.WriteLine($"Entity Type: {typeof(T).Name}, ID: {entity.Id}");
    }

    public T CreateDefault()
    {
        // Nhờ ràng buộc new(), ta có thể khởi tạo đối tượng T mới
        return new T();
    }
}