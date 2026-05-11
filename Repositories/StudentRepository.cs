using SchoolApi.Models;
namespace SchoolApi.Repositories;

public interface IStudentRepository
{
    // Define methods for CRUD operations on Student entities
    bool AddStudent(Student student);
    Student? GetStudentById(string id);
    IEnumerable<Student> GetAllStudents();
    Student? UpdateStudent(Student student);
    bool DeleteStudent(string id);
    bool EmailExists(string email);
}

public class StudentRepository : IStudentRepository
{
    private  List<Student> students;

    public StudentRepository()
    {
        students  = [
        new ("John Doe", "john.doe@example.com"),
        new ("Jane Smith", "jane.smith@example.com"),
        new ("Alice Johnson", "alice.johnson@example.com"),
        new ("Bob Brown", "bob.brown@example.com"),
        new ("Charlie Davis", "charlie.davis@example.com")
        ];
    }

    public bool AddStudent(Student student)
    {
        if (student == null) return false;
        
        students.Add(student); return true;

    }

    public Student? GetStudentById(string id)
    {
        return students.FirstOrDefault(s => s.StudentId == id);
    }

    public IEnumerable<Student> GetAllStudents()
    {
        return students;
    }

    public Student? UpdateStudent(Student student)
    {
        if (student == null)
        {
            return null;
        }

        var existing = GetStudentById(student.StudentId);
        if (existing == null)
        {
            return null;
        }

        existing.Name = student.Name;
        existing.Email = student.Email;
        return existing;
    }

    public bool DeleteStudent(string id)
    {
        var student = GetStudentById(id);
        if (student == null)
        {
            return false;
        }

        return students.Remove(student);
    }

    public bool EmailExists(string email)
{
    return students.Any(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
}

}

