using SchoolApi.Models;
using SchoolApi.Models.Requests;

namespace SchoolApi.Services;

public interface IGradeService
{
    List<Grade> GetGrades();
    Grade? GetGradeById(string id);
    List<Grade> GetGradesByStudent(string studentId);
    List<Grade> GetGradesByCourseInstance(string courseInstanceId);
    Grade? GetGradeByStudentAndCourseInstance(string studentId, string courseInstanceId);
    Grade CreateGrade(CreateGradeRequest request);
    Grade? UpdateGrade(string id, UpdateGradeRequest request);
    bool DeleteGrade(string id);
}

public class GradeService : IGradeService
{
    private readonly IStudentService _studentService;
    private readonly ICourseInstanceService _courseInstanceService;
    private List<Grade> _grades;

    public GradeService(IStudentService studentService, ICourseInstanceService courseInstanceService)
    {
        _studentService = studentService;
        _courseInstanceService = courseInstanceService;
        _grades = new List<Grade>();
        InitializeGrades();
    }

    private void InitializeGrades()
    {
        
        var students = _studentService.GetStudents();
        var courseInstances = _courseInstanceService.GetCourseInstances();
        
        if (students.Count >= 5 && courseInstances.Count >= 3)
        {
            _grades = new List<Grade>
            {
                new Grade("A", courseInstances[0], students[0]),
                new Grade("B", courseInstances[0], students[1]),
                new Grade("A-", courseInstances[1], students[2]),
                new Grade("B+", courseInstances[2], students[3]),
                new Grade("A", courseInstances[2], students[4])
            };
        }
    }

    public List<Grade> GetGrades()
    {
        return _grades;
    }

    public Grade? GetGradeById(string id)
    {
        return _grades.FirstOrDefault(g => g.GradeId == id);
    }

    public List<Grade> GetGradesByStudent(string studentId)
    {
        return _grades.Where(g => g.Student.StudentId == studentId).ToList();
    }

    public List<Grade> GetGradesByCourseInstance(string courseInstanceId)
    {
        return _grades.Where(g => g.CourseInstance.CourseInstanceId == courseInstanceId).ToList();
    }

    public Grade? GetGradeByStudentAndCourseInstance(string studentId, string courseInstanceId)
    {
        return _grades.FirstOrDefault(g => 
            g.Student.StudentId == studentId && 
            g.CourseInstance.CourseInstanceId == courseInstanceId);
    }

    public Grade CreateGrade(CreateGradeRequest request)
    {
        
        if (string.IsNullOrWhiteSpace(request.Value))
        {
            throw new ArgumentException("Grade value is required");
        }

        
        var student = _studentService.GetStudentById(request.StudentId);
        if (student == null)
        {
            throw new ArgumentException($"Student with ID {request.StudentId} not found");
        }

        
        var courseInstance = _courseInstanceService.GetById(request.CourseInstanceId);
        if (courseInstance == null)
        {
            throw new ArgumentException($"Course instance with ID {request.CourseInstanceId} not found");
        }

        
        var existingGrade = GetGradeByStudentAndCourseInstance(request.StudentId, request.CourseInstanceId);
        if (existingGrade != null)
        {
            throw new InvalidOperationException("Grade already exists for this student and course instance");
        }

        
        if (!courseInstance.Students.Any(s => s.StudentId == request.StudentId))
        {
            throw new InvalidOperationException("Student is not enrolled in this course instance");
        }

        var newGrade = new Grade(request.Value, courseInstance, student);
        _grades.Add(newGrade);
        return newGrade;
    }

    public Grade? UpdateGrade(string id, UpdateGradeRequest request)
    {
        var grade = GetGradeById(id);
        if (grade == null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(request.Value))
        {
            throw new ArgumentException("Grade value is required");
        }

        grade.Value = request.Value;
        return grade;
    }

    public bool DeleteGrade(string id)
    {
        var grade = GetGradeById(id);
        if (grade == null)
        {
            return false;
        }
        return _grades.Remove(grade);
    }
}