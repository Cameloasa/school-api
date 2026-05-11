//Services/CourseService.cs
namespace SchoolApi.Services;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

public interface ICourseService
{
    List<Course> GetCourses();
    Course? GetCourseById(string id);
    Course CreateCourse(CreateCourseRequest request);
    Course? UpdateCourse(string id, CreateCourseRequest request);
    bool DeleteCourse(string id);
}

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository repo)
    {
        _courseRepository = repo;
    }

    //get all courses
    public List<Course> GetCourses()
    {
        try{
            return _courseRepository.GetAllCourses().ToList();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while retrieving courses", ex);
        }      
    }

    //get course by id
    public Course? GetCourseById(string id)
    {
        try
        {
            // Validation
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Course ID cannot be empty");
            }
            
            return _courseRepository.GetCourseById(id);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while retrieving course with ID {id}", ex);
        }
    }

    //create course
    public Course CreateCourse(CreateCourseRequest request)
    {
        try
        {
            Course newCourse = new(request.Description, request.Title);
            _courseRepository.AddCourse(newCourse);
            return newCourse;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while creating the course", ex);
        }   
    }

    //update course
    public Course? UpdateCourse(string id, CreateCourseRequest request)
    {
        try
        {
            // Validations
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Course ID cannot be empty");
            }
            
            if (request.Equals(default(CreateCourseRequest)))
            {
                throw new ArgumentException("Request cannot be null");
            }

            // Find existing course
            var existingCourse = _courseRepository.GetCourseById(id);
            if (existingCourse == null)
            {
                return null;
            }
            // Update properties
            existingCourse.Title = request.Title;
            existingCourse.Description = request.Description;
            // Save to repository
            var updatedCourse = _courseRepository.UpdateCourse(existingCourse);
            return updatedCourse;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while updating course with ID {id}", ex);
        }
        
    }

    //delete course
    public bool DeleteCourse(string id)
    {
        try
        {
            // Validation
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Course ID cannot be empty");
            }
            
            return _courseRepository.DeleteCourse(id);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while deleting course with ID {id}", ex);
        }
    }
}