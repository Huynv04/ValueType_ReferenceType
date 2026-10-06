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
//tạo biến p2, p2=p1: copy địa chỉ lưu biến p1 cho p2 (cả 2 cùng trỏ vào 1 đối tượng trên Heap) 
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
 Bước 1: Khởi tạo biến number: Trên vùng nhớ Stack của luồng chính, hệ thống cấp phát một ô nhớ riêng cho number và lưu giá trị 5.

Bước 2: Gọi hàm TangGiaTri(number): Runtime tạo ra một Stack Frame (khung ngăn xếp) mới dành riêng cho hàm TangGiaTri. Tại đây, một biến tham số hoàn toàn mới tên là x được tạo ra ở một ô nhớ khác, và giá trị 5 từ number được sao chép sang x.

Bước 3: Thực hiện phép cộng x = x + 10: Lệnh này chỉ ghi đè con số 15 vào ô nhớ của x. Ô nhớ ban đầu của number nằm ở Stack Frame phía dưới hoàn toàn không bị tác động.

Bước 4: Hàm kết thúc: Khi gặp dấu đóng ngoặc }, toàn bộ Stack Frame của TangGiaTri bị hủy, biến x cùng giá trị 15 lập tức bị xóa khỏi bộ nhớ.

Bước 5: In kết quả: Lệnh Console.WriteLine(number) chỉ đọc dữ liệu từ ô nhớ của number, nơi giá trị vẫn là 5.
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
//void ResetPerson(Person p)
//{
//    p.Name = "Bình";      
//}
//Person p1 = new Person { Name = "Khang" };
//ResetPerson(p1);
//Console.WriteLine(p1.Name);
//public class Person
//{
//    public string Name { get; set; }
//}