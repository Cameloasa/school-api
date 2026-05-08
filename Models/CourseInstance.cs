namespace SchoolApi.Models;

public class CourseInstance
{
    public string CourseInstanceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Course Course { get; set; }
    public List<Student> Students { get; set; }

    public CourseInstance()

    {
        CourseInstanceId = Guid.NewGuid().ToString();
        Students = new List<Student>();
        Course = new Course("Default Course", "Default Description");
    }

    // Constructor with parameters  (used for creating new instances, ID is generated automatically)
    public CourseInstance(DateTime startDate, DateTime endDate, Course course, List<Student> students)
    {
        CourseInstanceId = Guid.NewGuid().ToString();
        
        // Validation fot date time
        if (startDate == default)
        {
            throw new ArgumentException("Start date is required.");
        }
        
        if (endDate == default)
        {
            throw new ArgumentException("End date is required.");
        }
        
        if (startDate >= endDate)
        {
            throw new ArgumentException("Start date must be before end date.");
        }
        
        if (course == null)
        {
            throw new ArgumentException("Course is required.");
        }
        
        if (students == null || !students.Any())
        {
            throw new ArgumentException("At least one student is required.");
        }
        
        StartDate = startDate;
        EndDate = endDate;
        Course = course;
        Students = students;
    }
    
    // Update constructor (takes existing ID)
    public CourseInstance(string id, DateTime startDate, DateTime endDate, Course course, List<Student> students)
    {
        // Validation for ID and date time
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("CourseInstance ID is required.");
        }
        
        CourseInstanceId = id;
        
        if (startDate == default)
        {
            throw new ArgumentException("Start date is required.");
        }
        
        if (endDate == default)
        {
            throw new ArgumentException("End date is required.");
        }
        
        if (startDate >= endDate)
        {
            throw new ArgumentException("Start date must be before end date.");
        }
        
        if (course == null)
        {
            throw new ArgumentException("Course is required.");
        }
        
        if (students == null || !students.Any())
        {
            throw new ArgumentException("At least one student is required.");
        }
        
        StartDate = startDate;
        EndDate = endDate;
        Course = course;
        Students = students;
    }
}