using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Mappers;

public static class CourseInstanceMapper
{
    public static CourseInstanceResponse ToResponse(CourseInstance ci)
    {
        return new CourseInstanceResponse
        {
            CourseInstanceId = ci.CourseInstanceId,
            CourseId = ci.Course?.CourseId ?? string.Empty,
            CourseTitle = ci.Course?.Title ?? string.Empty,
            StartDate = ci.StartDate,
            EndDate = ci.EndDate,
            Students = (ci.Students ?? new List<Student>())
                .Select(StudentMapper.ToResponse)
                .ToList()
        };
    }
}
