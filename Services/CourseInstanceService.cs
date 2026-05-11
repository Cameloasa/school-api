using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;
namespace SchoolApi.Services;

public interface ICourseInstanceService
{
    List<CourseInstance> GetCourseInstances();
    CourseInstance? GetCourseInstanceById(string id);
    CourseInstance CreateCourseInstance(CreateCourseInstancesRequest request);
    CourseInstance? UpdateCourseInstanceDate(string id, UpdateCourseInstanceRequest request);
    bool DeleteCourseInstance(string id);
    IEnumerable<CourseInstance> GetByStudent(string studentId); 
    IEnumerable<CourseInstance> GetByCourse(string courseId); 
    IEnumerable<CourseInstance> GetByDateRange(DateTime start, DateTime end);
}

public class CourseInstanceService : ICourseInstanceService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseInstanceRepository _courseInstanceRepository;

    public CourseInstanceService(IStudentRepository studentRepo, 
                                ICourseRepository courseRepo,
                                ICourseInstanceRepository courseInstanceRepo)
    {
        _studentRepository = studentRepo;
        _courseRepository = courseRepo;
        _courseInstanceRepository = courseInstanceRepo;
        
    }

    //get all course instances
    public List<CourseInstance> GetCourseInstances()
    {
        try
        {
            return _courseInstanceRepository.GetAllCourseInstances().ToList();
        }
        catch (Exception ex)    
        {
            throw new InvalidOperationException("An error occurred while retrieving course instances", ex);   
        }
    }

    //get course instance by id
    public CourseInstance? GetCourseInstanceById(string id)
    {
        try
        {
            // Validation
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Course instance ID cannot be empty");
            }
            return _courseInstanceRepository.GetCourseInstanceById(id);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while retrieving course instance with ID {id}", ex);
        }
    }

    //create course instance
    public CourseInstance CreateCourseInstance(CreateCourseInstancesRequest request)
    {
        try
        {
            // Validate course
            Course? course = _courseRepository.GetCourseById(request.CourseId) ?? 
            throw new ArgumentException($"Course with ID {request.CourseId} not found");

            // Validate students
            List<Student> students = [];
            foreach (string studentId in request.StudentIds)
            {
                Student? student = _studentRepository.GetStudentById(studentId) 
                ?? throw new ArgumentException($"Student with ID {studentId} not found");
                students.Add(student);
            }

            CourseInstance newInstance = new (  request.StartDate, 
                                                request.EndDate, 
                                                course,
                                                students);
            // Save to repository
            bool success = _courseInstanceRepository.AddCourseInstance(newInstance);
            if (!success)
            {
                throw new InvalidOperationException("Failed to create course instance");
            }
            // Return the created instance
            return newInstance;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while creating the course instance", ex);
        }
    }

    //update course instance
    public CourseInstance? UpdateCourseInstanceDate(string id, UpdateCourseInstanceRequest request)
    {
        try
        {
            
            return _courseInstanceRepository.UpdateCourseInstanceDate(id, request.StartDate, request.EndDate);
            
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while updating course instance with ID {id}", ex);
        }
    }
    //delete course instance
    public bool DeleteCourseInstance(string id)
    {
        try
        {
            // Validation
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Course instance ID cannot be empty");
            }
            
            return _courseInstanceRepository.DeleteCourseInstance(id);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while deleting course instance with ID {id}", ex);
        }
    }

    //get course instances by student id
    public IEnumerable<CourseInstance> GetByStudent(string studentId)
    {
        return _courseInstanceRepository.GetByStudentId(studentId);
    }

    public IEnumerable<CourseInstance> GetByCourse(string courseId)
    {
        return _courseInstanceRepository.GetByCourseId(courseId);
    }

    public IEnumerable<CourseInstance> GetByDateRange(DateTime start, DateTime end)
    {
        return _courseInstanceRepository.GetByDateRange(start, end);
    }
}