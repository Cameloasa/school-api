using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

namespace SchoolApi.Services;

public interface IGradeService
{
    IEnumerable<Grade> GetGrades();  // ← schimbat în IEnumerable
    Grade? GetGradeById(string id);
    IEnumerable<Grade> GetGradesByStudent(string studentId);
    IEnumerable<Grade> GetGradesByCourseInstance(string courseInstanceId);
    Grade? GetGradeByStudentAndCourseInstance(string studentId, string courseInstanceId);
    List<Grade> CreateGrade(CreateGradeRequest request);
    Grade? UpdateGrade(string id, UpdateGradeRequest request);
    bool DeleteGrade(string id);
}

public class GradeService : IGradeService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseInstanceRepository _courseInstanceRepository;
    private readonly IGradeRepository _gradeRepository;

    public GradeService(IStudentRepository studentRepo, 
                        ICourseInstanceRepository courseInstanceRepo,
                        IGradeRepository gradeRepo)
    {
        _studentRepository = studentRepo;
        _courseInstanceRepository = courseInstanceRepo;
        _gradeRepository = gradeRepo;
    }

    public IEnumerable<Grade> GetGrades()  // ← schimbat
    {
        try
        {
            return _gradeRepository.GetAllGrades().ToList();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while retrieving grades", ex);
        }
    }

    public Grade? GetGradeById(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Grade ID cannot be empty");
            
            return _gradeRepository.GetGradeById(id);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while retrieving the grade", ex);
        }
    }

    public IEnumerable<Grade> GetGradesByStudent(string studentId)  // ← schimbat
    {
        try
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty");
            
            return _gradeRepository.GetGradesByStudentId(studentId).ToList();
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while retrieving grades for student ID {studentId}", ex);
        }
    }

    public IEnumerable<Grade> GetGradesByCourseInstance(string courseInstanceId)  // ← schimbat
    {
        try
        {
            if (string.IsNullOrWhiteSpace(courseInstanceId))
                throw new ArgumentException("Course instance ID cannot be empty");
            
            return _gradeRepository.GetGradesByCourseInstanceId(courseInstanceId).ToList();
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while retrieving grades for course instance ID {courseInstanceId}", ex);
        }
    }

    public Grade? GetGradeByStudentAndCourseInstance(string studentId, string courseInstanceId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(courseInstanceId))
                throw new ArgumentException("Student ID and course instance ID cannot be empty");
            
            return _gradeRepository.GetGradeByStudentAndCourseInstanceId(studentId, courseInstanceId);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while retrieving the grade", ex);
        }
    }

    public List<Grade> CreateGrade(CreateGradeRequest request)
{
    if (string.IsNullOrWhiteSpace(request.CourseInstanceId))
        throw new ArgumentException("Course instance ID is required");

    var courseInstance = _courseInstanceRepository.GetCourseInstanceById(request.CourseInstanceId)
        ?? throw new ArgumentException($"Course instance {request.CourseInstanceId} not found");

    var validGrades = new List<string> { "A", "B", "C", "D", "E", "F" };

    var createdGrades = new List<Grade>();

    foreach (var entry in request.Grades)
    {
        if (string.IsNullOrWhiteSpace(entry.StudentId))
            throw new ArgumentException("Student ID is required");

        if (string.IsNullOrWhiteSpace(entry.Value))
            throw new ArgumentException("Grade value is required");

        if (!validGrades.Contains(entry.Value.ToUpper()))
            throw new ArgumentException("Grade must be one of: A, B, C, D, E, F");

        var student = _studentRepository.GetStudentById(entry.StudentId)
            ?? throw new ArgumentException($"Student {entry.StudentId} not found");

        if (!courseInstance.Students.Any(s => s.StudentId == entry.StudentId))
            throw new ArgumentException($"Student {entry.StudentId} is not enrolled in this course instance");

        var grade = new Grade(entry.Value, courseInstance, student);

        _gradeRepository.AddGrade(grade);
        createdGrades.Add(grade);
    }

    return createdGrades;
}


    public Grade? UpdateGrade(string id, UpdateGradeRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Grade ID cannot be empty");
            
            if (request.Equals(default(UpdateGradeRequest)))
                throw new ArgumentException("Request cannot be null");
            
            Grade? existingGrade = _gradeRepository.GetGradeById(id);
            if (existingGrade == null) 
                return null;

            if (!string.IsNullOrWhiteSpace(request.Value))
            {
                existingGrade.Value = request.Value;
            }

            return _gradeRepository.UpdateGrade(existingGrade);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while updating the grade with ID {id}", ex);
        }
    }

    public bool DeleteGrade(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Grade ID cannot be empty");
            
            return _gradeRepository.DeleteGrade(id);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while deleting the grade with ID {id}", ex);
        }
    }
}