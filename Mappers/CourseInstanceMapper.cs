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
            CourseId = ci.Course.CourseId,
            CourseTitle = ci.Course.Title,
            StartDate = ci.StartDate,
            EndDate = ci.EndDate,
            Students = ci.Students.Select(StudentMapper.ToResponse).ToList()
        };
    }
}
