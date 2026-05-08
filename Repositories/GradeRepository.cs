using SchoolApi.Models;

namespace SchoolApi.Repositories;

public interface IGradeRepository
{
    // CRUD de bază
    bool AddGrade(Grade grade);
    Grade? GetGradeById(string id);
    IEnumerable<Grade> GetAllGrades();
    Grade? UpdateGrade(Grade grade);
    bool DeleteGrade(string id);
    
    // Search specific
    IEnumerable<Grade> GetGradesByStudentId(string studentId);      
    IEnumerable<Grade> GetGradesByCourseInstanceId(string courseInstanceId);  
    Grade? GetGradeByStudentAndCourse(string studentId, string courseInstanceId);  
    
    // For updating just the grade value
    bool UpdateGradeValue(string studentId, string courseInstanceId, string newValue);
}

public class GradeRepository : IGradeRepository
{
    public bool AddGrade(Grade grade)
    {
        throw new NotImplementedException();
    }

    public bool DeleteGrade(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Grade> GetAllGrades()
    {
        throw new NotImplementedException();
    }

    public Grade? GetGradeById(string id)
    {
        throw new NotImplementedException();
    }

    public Grade? GetGradeByStudentAndCourse(string studentId, string courseInstanceId)
    {
        throw new NotImplementedException();
    }

    public Grade? GetGradeByStudentId(string studentId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Grade> GetGradesByCourseInstanceId(string courseInstanceId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Grade> GetGradesByStudentId(string studentId)
    {
        throw new NotImplementedException();
    }

    public Grade? UpdateGrade(Grade grade)
    {
        throw new NotImplementedException();
    }

    public bool UpdateGradeValue(string studentId, string courseInstanceId, string newValue)
    {
        throw new NotImplementedException();
    }
}

