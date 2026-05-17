using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Mappers;

public static class GradeMapper
{
    public static GradeResponse ToResponse(Grade g)
    {
        return new GradeResponse
        {
            GradeId = g.GradeId,
            Value = g.Value,

            StudentId = g.StudentId,
            StudentFirstName = g.Student?.FirstName?? "Unknown",
            StudentLastName = g.Student?.LastName?? "Unknown",

            CourseInstanceId = g.CourseInstanceId,
            CourseTitle = g.CourseInstance?.Course?.Title ?? "Unknown"
        };
    }
}


