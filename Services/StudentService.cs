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
    
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository repo)
    {
        _studentRepository = repo;
    }
    // get all students
    public List<Student> GetStudents()
    {
        return (List<Student>)_studentRepository.GetAllStudents();
    }

    //get students by Id
    public Student? GetStudentById(string id)
    {
        Student? found = _studentRepository.GetStudentById(id);
        return found;
    }

    // create student
    public Student CreateStudent(CreateStudentRequest request)
    {
        Student newStudent = new(request.Name, request.Email);
        bool success = _studentRepository.AddStudent(newStudent);
        if (!success)        {
            throw new Exception("Failed to create student");
        }
        return newStudent;
    }

    //update student by Id
    public Student? UpdateStudent(string id, CreateStudentRequest request)
    {
        Student? found = _studentRepository.GetStudentById(id);
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
        Student? found = _studentRepository.GetStudentById(id);
        if(found == null)
        {
            return false;
        }
        return true;
    }


}


