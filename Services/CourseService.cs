
using SchoolApi.Mappers;
using SchoolApi.Models;
using SchoolApi.Models.DTOs;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

namespace SchoolApi.Services;


// =========================
//      INTERFACE
// =========================
public interface ICourseService
{
    Task<List<CourseResponse>> GetCoursesAsync();
    Task<CourseResponse?> GetCourseByIdAsync(string id);
    Task<CourseResponse> CreateCourseAsync(CreateCourseRequest request);
    Task<CourseResponse?> UpdateCourseAsync(string id, UpdateCourseRequest request);
    Task<bool> DeleteCourseAsync(string id);
}

// =========================
//      IMPLEMENTATION
// =========================
public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository repo)
    {
        _courseRepository = repo;
    }


    //get all
    public async Task<List<CourseResponse>> GetCoursesAsync()
    {
        var courses = await _courseRepository.GetCoursesAsync();
        return courses.Select(CourseMapper.ToResponse).ToList();
    }

    // get by id
    public async Task<CourseResponse?> GetCourseByIdAsync(string id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        return course is null ? null : CourseMapper.ToResponse(course);
    }

    //create
    public async Task<CourseResponse> CreateCourseAsync(CreateCourseRequest request)
    {
        var existing = await _courseRepository.GetByTitleAsync(request.Title);
        if (existing != null)
        throw new Exception($"A course with title '{request.Title}' already exists");
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description
        };

        var created = await _courseRepository.AddCourseAsync(course);
        return CourseMapper.ToResponse(created);
    }

    //update
    public async Task<CourseResponse?> UpdateCourseAsync(string id, UpdateCourseRequest request)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        if (course == null) return null;

        // PATCH logic

        if (!string.IsNullOrWhiteSpace(request.Description))
            course.Description = request.Description;

        var updated = await _courseRepository.UpdateCourseAsync(course);
        return updated is null ? null : CourseMapper.ToResponse(updated);
    }

    //delete
    public async Task<bool> DeleteCourseAsync(string id)
    {
        return await _courseRepository.DeleteCourseAsync(id);
    }
}