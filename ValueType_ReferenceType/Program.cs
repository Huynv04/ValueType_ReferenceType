//value type
//int a = 10;
//int b = a; // Copy giá trị 10 từ 'a' sang ô nhớ mới của 'b'

//b = 20;    // Chỉ sửa giá trị tại ô nhớ của 'b'

//Console.WriteLine(a); // In ra: 10 (a hoàn toàn không bị ảnh hưởng)
//Console.WriteLine(b); // In ra: 20

//Reference type
// Định nghĩa một class (Reference Type)

/*
 biến p1 , name=An 
tạo biến p2, p2=p1: copy địa chỉ lưu biến p1 cho p2 (cả 2 cùng trỏ vào 1 đối tượng trên Heap)
 */
Person p1 = new Person();
//Person p1 = new Person();
p1.Name = "An";

Person p2 = p1; // Copy 'địa chỉ' của p1 sang p2 (cả 2 cùng trỏ vào 1 đối tượng trên Heap)

p2.Name = "Bình"; // Sửa dữ liệu của đối tượng mà p2 đang trỏ tới

Console.WriteLine(p1.Name); // In ra: Bình (vì p1 và p2 dùng chung một vùng nhớ!)
Console.WriteLine(p2.Name); // In ra: Bình

public class Person
{
    public string Name { get; set; }
}