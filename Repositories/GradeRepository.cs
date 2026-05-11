using SchoolApi.Models;

namespace SchoolApi.Repositories;

public interface IGradeRepository
{
    // CRUD 
    bool AddGrade(Grade grade);
    Grade? GetGradeById(string id);
    IEnumerable<Grade> GetAllGrades();
    Grade? UpdateGrade(Grade grade);
    bool DeleteGrade(string id);
    
    // Search specific
    IEnumerable<Grade> GetGradesByStudentId(string studentId);      
    IEnumerable<Grade> GetGradesByCourseInstanceId(string courseInstanceId);  
    Grade? GetGradeByStudentAndCourseInstanceId(string studentId, string courseInstanceId);  
}

public class GradeRepository : IGradeRepository
{
    private List<Grade> grades;

    public GradeRepository()
    {
        grades = [];// grades = new List<Grade>(); empty list
    }

    //create
    public bool AddGrade(Grade grade)
    {
        if (grade == null) return false;
        grades.Add(grade); 
        return true;
    }

    public bool DeleteGrade(string id)
    {
        var existing = GetGradeById(id);
        if (existing == null) return false;
        return grades.Remove(existing);
    }

    public IEnumerable<Grade> GetAllGrades()
    {
        return grades;
    }

    public Grade? GetGradeById(string id)
    {
        return grades.FirstOrDefault(g => g.GradeId == id);
    }

    // Search student + course instance
    public Grade? GetGradeByStudentAndCourseInstanceId(string studentId, string courseInstanceId)
    {
        return grades.FirstOrDefault(g => g.Student?.StudentId == studentId && 
                                         g.CourseInstance?.CourseInstanceId == courseInstanceId);
    }

    // all grades for a student
    public IEnumerable<Grade> GetGradesByStudentId(string studentId)
    {
        return grades.Where(g => g.Student?.StudentId == studentId);
    }

    // all grades for a course instance
    public IEnumerable<Grade> GetGradesByCourseInstanceId(string courseInstanceId)
    {
        return grades.Where(g => g.CourseInstance?.CourseInstanceId == courseInstanceId);
    }

    // Update
    public Grade? UpdateGrade(Grade grade)
    {
        if (grade == null) return null;
        
        var existing = GetGradeById(grade.GradeId);
        if (existing == null) return null;
        
        existing.Value = grade.Value;
        existing.Student = grade.Student;
        existing.CourseInstance = grade.CourseInstance;
        
        return existing;
    }
}
