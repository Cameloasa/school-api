using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Mappers;

public static class CourseMapper
{
    public static CourseResponse ToResponse(Course c)
    {
        return new CourseResponse
        {
            CourseId = c.CourseId,
            Title = c.Title,
            Description = c.Description
        };
    }
}

