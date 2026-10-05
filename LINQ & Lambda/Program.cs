public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Score { get; set; }
    public string Major { get; set; }
}

List<Student> students = new List<Student>
{
    new Student { Name = "An", Age = 20, Score = 8.5, Major = "IT" },
    new Student { Name = "Bình", Age = 22, Score = 6.0, Major = "Business" },
    new Student { Name = "Cường", Age = 21, Score = 9.0, Major = "IT" },
    new Student { Name = "Dung", Age = 20, Score = 7.5, Major = "Design" },
    new Student { Name = "Giang", Age = 23, Score = 5.0, Major = "IT" }
};

// Sắp xếp sinh viên giảm dần theo Score, nếu trùng điểm thì sắp xếp tăng dần theo Tuổi
var sortedStudents = students
    .OrderByDescending(s => s.Score)
    .ThenBy(s => s.Age)
    .ToList();

// Gom nhóm sinh viên theo Chuyên ngành (Major)
var groupedByMajor = students
    .GroupBy(s => s.Major);

foreach (var group in groupedByMajor)
{
    Console.WriteLine($"--- Ngành: {group.Key} (Số lượng: {group.Count()}) ---");
    foreach (var student in group)
    {
        Console.WriteLine($" + {student.Name} ({student.Score} điểm)");
    }
}