using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Mappers;
public static class GradeMapper
{
    public static GradeDTO ToDTO(Grade g)
    {
        return new GradeDTO
        {
            GradeId = g.GradeId,
            Value = g.Value,
            StudentId = g.Student.StudentId,
            StudentFullName = g.Student.FullName,
            CourseInstanceId = g.CourseInstance.CourseInstanceId,
            CourseTitle = g.CourseInstance.Course.Title
        };
    }
}
