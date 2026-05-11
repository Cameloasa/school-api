
using SchoolApi.Models;

namespace SchoolApi.Repositories;

public interface IGradeRepository
{
    // CRUD 
    bool AddGrade(Grade grade);
    Grade? GetGradeById(string id);
    IEnumerable<Grade> GetAllGrades();
    Grade? UpdateGradeValue(string gradeId, string studentId, string courseId, string newValue);
    bool DeleteGrade(string id);
    
    // Search specific
    IEnumerable<Grade> GetGradesByStudentId(string studentId);      
    IEnumerable<Grade> GetGradesByCourseInstanceId(string courseInstanceId); 
    IEnumerable<Grade> GetGradesByStudentAndCourseId(string studentId, string courseId); 
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

    // delete grade by ID
    public bool DeleteGrade(string id)
    {
        var grade = GetGradeById(id);
        if (grade == null) return false;
        return grades.Remove(grade);
    }

    // read all grades
    public IEnumerable<Grade> GetAllGrades()
    {
        return grades;
    }

    // read grade by ID
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

    // Update value of a grade for a student (assuming one grade per student per course instance)
    public Grade? UpdateGradeValue(string gradeId, string studentId, string courseId, string newValue)
    {
         // Search for the grade by ID
        Grade? existing = GetGradeById(gradeId);
        if (existing == null) return null;

        // 2. Verify that the grade belongs to the correct student
        if (existing.Student.StudentId != studentId)
            return null;

        // 3. Verify that the grade belongs to the correct course
        if (existing.CourseInstance.Course.CourseId != courseId)
            return null;

        // 4. Update the value
        existing.Value = newValue;
        return existing;
        // save changes if using a real database context
    }

    // Search for grades by student and course (not instance)
    public IEnumerable<Grade> GetGradesByStudentAndCourseId(string studentId, string courseId)
    {
        return grades.Where(g => g.Student?.StudentId == studentId && 
                                 g.CourseInstance?.Course?.CourseId == courseId);
    }
}
