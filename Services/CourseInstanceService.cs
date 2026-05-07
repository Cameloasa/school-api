using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

public interface ICourseInstanceService
{
    List<CourseInstance> GetCourseInstances();
    CourseInstance? GetById(int id);
    CourseInstance CreateCourseInstance(CreateCourseInstancesRequest request);
    CourseInstance? UpdateCourseInstance(int id, CreateCourseInstancesRequest request);
    bool DeleteCourseInstance(int id);
    IEnumerable<CourseInstance> GetByStudent(int studentId); 
    IEnumerable<CourseInstance> GetByCourse(int courseId); 
}

public class CourseInstanceService : ICourseInstanceService
{
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;
    private List<CourseInstance> courseInstances = new List<CourseInstance>();

    public CourseInstanceService(IStudentService studentService, ICourseService courseService)
    {
        _studentService = studentService;
        _courseService = courseService;
        
        InitializeCourseInstances();
    }

    private void InitializeCourseInstances()
    {

        var students = _studentService.GetStudents();
        var courses = _courseService.GetCourses();
        
        if (students.Count >= 2 && courses.Count >= 1)
        {
            courseInstances = new List<CourseInstance>
            {
                new CourseInstance(
                    DateTime.Now.AddMonths(1).Date, 
                    DateTime.Now.AddMonths(3).Date, 
                    courses[0], 
                    new List<Student> { students[0], students[1] }
                ),
                new CourseInstance(
                    DateTime.Now.AddMonths(2).Date, 
                    DateTime.Now.AddMonths(4).Date, 
                    courses[1 % courses.Count], 
                    new List<Student> { students[2], students[3] }
                ),
                new CourseInstance(
                    DateTime.Now.AddMonths(1).Date, 
                    DateTime.Now.AddMonths(3).Date, 
                    courses[2 % courses.Count],
                    new List<Student> { students[3], students[4 % students.Count] }
                ),
            };
        }
    }

    public List<CourseInstance> GetCourseInstances()
    {
        return courseInstances;
    }

    public CourseInstance? GetById(int id)
    {
        return courseInstances.FirstOrDefault(i => i.CourseInstanceId == id);
    }

    public CourseInstance CreateCourseInstance(CreateCourseInstancesRequest request)
    {
        
        Course? course = _courseService.GetCourseById(request.CourseId);
        if (course == null)
        {
            throw new ArgumentException($"Course with ID {request.CourseId} not found");
        }

        
        List<Student> students = new List<Student>();
        foreach (int studentId in request.StudentId)
        {
            Student? student = _studentService.GetStudentById(studentId);
            if (student == null)
            {
                throw new ArgumentException($"Student with ID {studentId} not found");
            }
            students.Add(student);
        }

    
        CourseInstance newCourseInstance = new CourseInstance(
            request.StartDate,
            request.EndDate,
            course,
            students
        );

        courseInstances.Add(newCourseInstance);
        return newCourseInstance;
    }

    public CourseInstance? UpdateCourseInstance(int id, CreateCourseInstancesRequest request)
    {
        CourseInstance? existing = GetById(id);
        if (existing == null)
        {
            return null;
        }

        if (request.StartDate >= request.EndDate)
        {
            throw new ArgumentException("Start date must be before end date");
        }

        Course? course = _courseService.GetCourseById(request.CourseId);
        if (course == null)
        {
            throw new ArgumentException($"Course with ID {request.CourseId} not found");
        }

        List<Student> students = new List<Student>();
        foreach (int studentId in request.StudentId)
        {
            Student? student = _studentService.GetStudentById(studentId);
            if (student == null)
            {
                throw new ArgumentException($"Student with ID {studentId} not found");
            }
            students.Add(student);
        }

        existing.StartDate = request.StartDate;
        existing.EndDate = request.EndDate;
        existing.Course = course;
        existing.Students = students;

        return existing;
    }

    public bool DeleteCourseInstance(int id)
    {
        CourseInstance? existing = GetById(id);
        if (existing == null)
        {
            return false;
        }
        return courseInstances.Remove(existing);
    }

    public IEnumerable<CourseInstance> GetByStudent(int studentId)
    {
        return courseInstances.Where(c => c.Students.Any(s => s.StudentId == studentId));
    }

    public IEnumerable<CourseInstance> GetByCourse(int courseId)
    {
        return courseInstances.Where(c => c.Course.CourseId == courseId);
    }
}