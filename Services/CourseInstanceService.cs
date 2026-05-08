using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;
namespace SchoolApi.Services;

public interface ICourseInstanceService
{
    List<CourseInstance> GetCourseInstances();
    CourseInstance? GetCourseInstanceById(string id);
    CourseInstance CreateCourseInstance(CreateCourseInstancesRequest request);
    CourseInstance? UpdateCourseInstance(string id, CreateCourseInstancesRequest request);
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
            // Validations date
            if (request.StartDate < DateTime.Now.Date)
            {
                throw new ArgumentException("Start date must be in the future");
            }
            if (request.StartDate >= request.EndDate)
            {
                throw new ArgumentException("Start date must be before end date");
            }
            if(request.EndDate < DateTime.Now)
            {
                throw new ArgumentException("End date must be in the future");
            }

            // Validate course
            Course? course = _courseRepository.GetCourseById(request.CourseId) ?? 
            throw new ArgumentException($"Course with ID {request.CourseId} not found");

            // Validate students
            List<Student> students = [];
            foreach (string studentId in request.StudentId)
            {
                Student? student = _studentRepository.GetStudentById(studentId) 
                ?? throw new ArgumentException($"Student with ID {studentId} not found");
                students.Add(student);
            }

            CourseInstance newInstance = new (request.StartDate, 
                                                request.EndDate, 
                                                course,
                                                students);
            // Save to repository
            _courseInstanceRepository.AddCourseInstance(newInstance);
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
    public CourseInstance? UpdateCourseInstance(string id, CreateCourseInstancesRequest request)
    {
        try
        {
            // Validations
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Course instance ID cannot be empty");
            }
            
            if (request.StartDate >= request.EndDate)
            {
                throw new ArgumentException("Start date must be before end date");
            }

            CourseInstance? existing = _courseInstanceRepository.GetCourseInstanceById(id);
            if (existing == null)
            {
                return null;
            }

            Course? course = _courseRepository.GetCourseById(request.CourseId) ?? throw new ArgumentException($"Course with ID {request.CourseId} not found");

            // Validate students
            List<Student> students = [];
            foreach (string studentId in request.StudentId)
            {
                Student? student = _studentRepository.GetStudentById(studentId) ?? throw new ArgumentException($"Student with ID {studentId} not found");
                students.Add(student);
            }

            existing.StartDate = request.StartDate;
            existing.EndDate = request.EndDate;
            existing.Course = course;
            existing.Students = students;

            return _courseInstanceRepository.UpdateCourseInstance(existing);
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