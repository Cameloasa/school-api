using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Mappers;

public static class StudentMapper
{
    public static StudentDTO ToDTO(Student s)
    {
        return new StudentDTO
        {
            StudentId = s.StudentId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            FullName = s.FullName,
            Email = s.Email
        };
    }
}
