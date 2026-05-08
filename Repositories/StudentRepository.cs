using SchoolApi.Models;


public interface IStudentRepository
{
    // Define methods for CRUD operations on Student entities
    bool AddStudent(Student student);
    Student? GetStudentById(string id);
    IEnumerable<Student> GetAllStudents();
    bool UpdateStudent(Student student);
    bool DeleteStudent(string id);
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
        try
        {
            students.Add(student);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Student? GetStudentById(string id)
    {
        return students.Find(s => s.StudentId == id);
    }

    public IEnumerable<Student> GetAllStudents()
    {
        return students;
    }

    public bool UpdateStudent(Student student)
    {
        if (student == null)
        {
            return false;
        }

        var existing = GetStudentById(student.StudentId);
        if (existing == null)
        {
            return false;
        }

        var index = students.IndexOf(existing);
        students[index] = student;
        return true;
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
}

