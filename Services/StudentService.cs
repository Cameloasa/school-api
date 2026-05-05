namespace SchoolApi.Services;
using SchoolApi.Models;
using SchoolApi.Models.Requests;


public interface IStudentservice{
    List<Student> GetStudents();
    Student? GetStudentById(int id);
    Student CreateStudent(CreateStudentRequest request);
    Student? UpdateStudent(int id, CreateStudentRequest request);
    bool DeleteStudent(int id);
}

public class StudentService: IStudentservice
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
    public Student? GetStudentById(int id)
    {
        Student? found = students.FirstOrDefault(s => s.Id == id);
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
    public Student? UpdateStudent(int id, CreateStudentRequest request)
    {
        Student? found = students.FirstOrDefault(s => s.Id == id);
        if(found == null)
        {
            return null;
        }
        found.Name = request.Name;
        found.Email = request.Email;
        return found;
    }

    //delete student
    public bool DeleteStudent(int id)
    {
        Student? found = students.FirstOrDefault(s => s.Id == id);
        if(found == null)
        {
            return false;
        }
        students.Remove(found);
        return true;
    }


}


