/*value type
int a = 10;
int b = a; // Copy giá trị 10 từ 'a' sang ô nhớ mới của 'b'

b = 20;    // Chỉ sửa giá trị tại ô nhớ của 'b'

Console.WriteLine(a); // In ra: 10 (a hoàn toàn không bị ảnh hưởng)
Console.WriteLine(b); // In ra: 20
*/

/*Reference type
 biến p1 , name=An 
tạo biến p2, p2=p1: copy địa chỉ lưu biến p1 cho p2 (cả 2 cùng trỏ vào 1 đối tượng trên Heap)
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
 */
/*Pass by value
Mỗi khi hàm đc gọi, hệ thống cấp phát riêng 1 ngăn kéo trên vùng nhớ Stack, ngăn kéo này chứa:
Các tham số truyền vào hàm.
Các biến cục bộ khai báo bên trong hàm.

Khi gọi TangGiaTri(number):
Máy tính đọc giá trị hiện tại của biến number (là con số 5).

Nó tạo ra một ô nhớ hoàn toàn mới tên là x nằm trong ngăn kéo của hàm TangGiaTri.

Nó chép con số 5 đó bỏ vào ô x.

Biến number và biến x là hai biến riêng biệt, nằm ở hai ô nhớ hoàn toàn khác nhau. Phép gán bên trong hàm chỉ sửa ô nhớ x, không chạm vào ô nhớ number.

 Vùng nhớ của hàm: Là phân vùng nhớ riêng (Stack Frame) được cấp phát tạm thời cho hàm khi nó đang thực thi.

Bản sao: Biến tham số x là một ô nhớ mới nhận giá trị copy từ number. Mọi thao tác cộng trừ đều xảy ra trên x và biến mất khi hàm kết thúc.
 */

//using System;

//int number = 5;
//TangGiaTri(number);

//Console.WriteLine(number); // Vẫn in ra: 5 (number không hề đổi)
//void TangGiaTri(int x)
//{
//    x = x + 10; // Chỉ thay đổi bản sao 'x' trên vùng nhớ của hàm TangGiaTri
//}

/*
Ban đầu (Trước khi gọi hàm)
Bạn tạo p1 = new Person { Name = "Khang" }.
Đối tượng thực tế (tạm gọi là Đối tượng A) được tạo trên Heap với Name = "Khang", nằm tại địa chỉ giả định 0xAA.
Biến p1 nằm trên Stack của Main, giữ giá trị là địa chỉ 0xAA

Khi gọi ResetPerson(p1)
Hàm mở một Stack Frame mới.
Một biến tham số mới tên là p được tạo ra trên Stack của hàm ResetPerson.
Hệ thống copy địa chỉ 0xAA từ p1 bỏ vào p. Lúc này cả p1 và p đều trỏ chung vào Đối tượng A.

Khi chạy dòng p.Name = "Bình";
Lệnh này lần theo địa chỉ 0xAA mà p đang giữ để tìm đến Đối tượng A trên Heap và đổi thuộc tính Name thành "Bình".

Vì p1 cũng đang giữ địa chỉ 0xAA, nên nếu đọc qua p1 lúc này, Name đã là "Bình".

Khi chạy dòng p = new Person(); và p.Name = "Cường";
Lệnh new Person() tạo ra một Đối tượng B hoàn toàn mới trên Heap tại địa chỉ 0xBB.

Phép gán p = ... ghi đè địa chỉ mới 0xBB vào ô nhớ của biến p.

Biến p1 ở ngoài hoàn toàn không bị ảnh hưởng vì nó nằm ở Stack Frame của Main, vẫn giữ nguyên địa chỉ 0xAA.

Dòng tiếp theo p.Name = "Cường" chỉ tác động lên Đối tượng B (0xBB).

Khi hàm kết thúc và in kết quả
Hàm ResetPerson kết thúc, biến p bị xóa khỏi Stack. Đối tượng B không còn ai trỏ tới và sẽ bị GC thu hồi.

Chương trình quay lại Main và chạy Console.WriteLine(p1.Name);.

Biến p1 vẫn luôn trỏ tới Đối tượng A (0xAA) từ đầu đến cuối, mà giá trị cuối cùng được ghi vào Đối tượng A chính là "Bình".
Sửa thuộc tính của đối tượng (p.Property = ...): Thay đổi nội dung bên trong Heap => Ảnh hưởng ra ngoài.Gán đối tượng mới (p = new ...): Chỉ đổi địa chỉ lưu trong biến tham số cục bộ trên Stack $\rightarrow$ Không ảnh hưởng ra ngoài.


 */
void ResetPerson(Person p)
{
    p.Name = "Bình";       // Sửa thuộc tính: Ảnh hưởng bên ngoài (vì chung địa chỉ Heap)
    p = new Person();      // Gán đối tượng mới: KHÔNG ảnh hưởng bên ngoài!
    p.Name = "Cường";      // Chỉ đổi trên đối tượng mới tạo trong hàm
}
Person p1 = new Person { Name = "Khang" };
ResetPerson(p1);
Console.WriteLine(p1.Name);
public class Person
{
    public string Name { get; set; }
}