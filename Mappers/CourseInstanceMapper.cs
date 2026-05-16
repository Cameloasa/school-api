using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Mappers;
public static class CourseInstanceMapper
{
    public static CourseInstanceDTO ToDTO(CourseInstance ci)
    {
        return new CourseInstanceDTO
        {
            CourseInstanceId = ci.CourseInstanceId,
            CourseId = ci.Course.CourseId,
            CourseTitle = ci.Course.Title,
            Students = ci.Students.Select(StudentMapper.ToDTO).ToList()
        };
    }
}
