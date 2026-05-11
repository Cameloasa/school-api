using SchoolApi.Models;
namespace SchoolApi.Repositories;

public interface IStudentRepository
{
    // Define methods for CRUD operations on Student entities
    bool AddStudent(Student student);
    Student? GetStudentById(string id);
    IEnumerable<Student> GetAllStudents();
    Student? UpdateStudentName(string studentId, string newName);
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

    // Create a new student
    public bool AddStudent(Student student)
    {
        if (student == null) return false;
        
        students.Add(student); return true;

    }

    // Read a student by ID
    public Student? GetStudentById(string id)
    {
        return students.FirstOrDefault(s => s.StudentId == id);
    }

    // Read all students
    public IEnumerable<Student> GetAllStudents()
    {
        return students;
    }

    public Student? UpdateStudentName(string studentId, string newName)
    {
        Student? existing = GetStudentById(studentId);
        if (existing == null)
        {
            return null;
        }

        existing.Name = newName;
        return existing;
        // save changes if using a real database context
    }

    // Delete a student by ID
    public bool DeleteStudent(string id)
    {
        var student = GetStudentById(id);
        if (student == null) return false;
        
        return students.Remove(student);
    }

    // Check if an email already exists in the repository
    public bool EmailExists(string email)
    {
        return students.Any(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

}

