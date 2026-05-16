using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

namespace SchoolApi.Services;

public interface IGradeService
{
    IEnumerable<Grade> GetGrades();  
    Grade? GetGradeById(string id);
    IEnumerable<Grade> GetGradesByStudent(string studentId);
    IEnumerable<Grade> GetGradesByCourseId(string courseId);
    IEnumerable<Grade> GetGradesByCourseInstance(string courseInstanceId);
    Grade? GetGradeByStudentAndCourseInstance(string studentId, string courseInstanceId);
    List<Grade> CreateGrade(CreateGradeRequest request);
    Grade? UpdateGradeValue(string studentId, string courseId, UpdateGradeRequest request);
    bool DeleteGrade(string id);
}

public class GradeService : IGradeService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseInstanceRepository _courseInstanceRepository;
    private readonly IGradeRepository _gradeRepository;

    public GradeService(IStudentRepository studentRepo, 
                        ICourseRepository courseRepo,
                        ICourseInstanceRepository courseInstanceRepo,
                        IGradeRepository gradeRepo)
    {
        _studentRepository = studentRepo;
        _courseRepository = courseRepo;
        _courseInstanceRepository = courseInstanceRepo;
        _gradeRepository = gradeRepo;
    }

    //get grades
    public IEnumerable<Grade> GetGrades() 
    {
        try
        {
            return [.. _gradeRepository.GetAllGrades()];
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while retrieving grades", ex);
        }
    }

    //get grade by id
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

    //get grades by student id
    public IEnumerable<Grade> GetGradesByStudent(string studentId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty");
            
            return [.. _gradeRepository.GetGradesByStudentId(studentId)];
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

    //get grades by course instance id
    public IEnumerable<Grade> GetGradesByCourseInstance(string courseInstanceId) 
    {
        try
        {
            if (string.IsNullOrWhiteSpace(courseInstanceId))
                throw new ArgumentException("Course instance ID cannot be empty");
            
            return [.. _gradeRepository.GetGradesByCourseInstanceId(courseInstanceId)];
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

    //get grade by student id and course instance id
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

   //create grade
   public List<Grade> CreateGrade(CreateGradeRequest request)
    {
        try
        {
            var validGradesValues = new List<string> { "A", "B", "C", "D", "E", "F" };
            var createdGrades = new List<Grade>();

            // 1. CourseInstance must exist
            var courseInstance = _courseInstanceRepository.GetCourseInstanceById(request.CourseInstanceId)
                ?? throw new ArgumentException($"Course instance {request.CourseInstanceId} not found");

            // 2. Iterate through entries
            foreach (var entry in request.Grades)
            {
                // Validate grade value
                if (!validGradesValues.Contains(entry.Value.ToUpper()))
                    throw new ArgumentException("Grade must be one of: A, B, C, D, E, F");

                // Validate student
                var student = _studentRepository.GetStudentById(entry.StudentId)
                    ?? throw new ArgumentException($"Student {entry.StudentId} not found");

                // 3. Create grade
                var newGrade = new Grade(entry.Value, courseInstance, student);

                // 4. Save to repo
                bool success = _gradeRepository.AddGrade(newGrade);

                if (!success)
                    throw new InvalidOperationException("Failed to create grade");

                // 5. Add to list
                createdGrades.Add(newGrade);
            }

            return createdGrades;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while creating the grade", ex);
        }
    }

    //update grade value
    public Grade? UpdateGradeValue(string studentId, string courseId, UpdateGradeRequest request)
    {
        try
        {
            return _gradeRepository.UpdateGradeValue(studentId, courseId, request.Value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"An error occurred while updating grade for student {studentId} at course {courseId}", ex);
        }
    }

    //delete grade
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

    //get grade by course Id
    public IEnumerable<Grade> GetGradesByCourseId(string courseId)
    {
        try
        {
            return [.. _gradeRepository.GetGradesByCourseId(courseId)];
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while retrieving grades for course ID {courseId}", ex);
        }
    }

    // helper for finding active course instances
    private CourseInstance GetActiveInstanceOrThrow(string courseId)
{
        var instance = _courseInstanceRepository.GetActiveInstanceByCourseId(courseId) 
        ?? throw new Exception("No active course instance found for this course.");
        return instance;
}

}