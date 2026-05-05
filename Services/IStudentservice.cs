
using SchoolApi.Models;
using SchoolApi.Models.Requests;

namespace SchoolApi.Services
public interface IStudentservice
{
    List<Student> GetStudents();
    Student? GetStudentById(int id);
    Student CreateStudent(CreateStudentRequest request);
    Student? UpdateStudent(int id, CreateStudentRequest request);
    bool DeleteStudent(int id);

}
}


