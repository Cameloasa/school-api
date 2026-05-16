using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Mappers;
public static class CourseMapper
{
    public static CourseDTO ToDTO(Course c)
    {
        return new CourseDTO
        {
            CourseId = c.CourseId,
            Title = c.Title,
            Description = c.Description
        };
    }
}
