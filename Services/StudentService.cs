namespace SchoolApi.Services;
using SchoolApi.Models;
using SchoolApi.Models.Requests;


public interface IStudentService{
    List<Student> GetStudents();
    Student? GetStudentById(string id);
    Student CreateStudent(CreateStudentRequest request);
    Student? UpdateStudent(string id, CreateStudentRequest request);
    bool DeleteStudent(string id);
}

public class StudentService: IStudentService
{
    //Global students list
    List<Student> students = [
        new ("John Doe", "john.doe@example.com"),
        new ("Jane Smith", "jane.smith@example.com"),
        new ("Alice Johnson", "alice.johnson@example.com"),
        new ("Bob Brown", "bob.brown@example.com"),
        new ("Charlie Davis", "charlie.davis@example.com")
    ];

    // get all students
    public List<Student> GetStudents()
    {
        return students;
    }

    //get students by Id
    public Student? GetStudentById(string id)
    {
        Student? found = students.FirstOrDefault(s => s.StudentId == id);
        return found;
    }

    // create student
    public Student CreateStudent(CreateStudentRequest request)
    {
        Student newStudent = new(request.Name, request.Email);
        students.Add(newStudent);
        return newStudent;
    }

    //update student by Id
    public Student? UpdateStudent(string id, CreateStudentRequest request)
    {
        Student? found = students.FirstOrDefault(s => s.StudentId == id);
        if(found == null)
        {
            return null;
        }
        found.Name = request.Name;
        found.Email = request.Email;
        return found;
    }

    //delete student
    public bool DeleteStudent(string id)
    {
        Student? found = students.FirstOrDefault(s => s.StudentId == id);
        if(found == null)
        {
            return false;
        }
        students.Remove(found);
        return true;
    }


}


