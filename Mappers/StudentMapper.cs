using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Mappers;

public static class StudentMapper
{
    public static StudentResponse ToResponse(Student s)
    {
        return new StudentResponse
        {
            StudentId = s.StudentId,
            FullName = s.FullName,
            Email = s.Email
        };
    }
}
