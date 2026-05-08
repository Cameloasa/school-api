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
        Course = new Course();
    }

    // Constructor with parameters  (used for creating new instances, ID is generated automatically)
    public CourseInstance(DateTime startDate, DateTime endDate, Course course, List<Student> students)
    {
        CourseInstanceId = Guid.NewGuid().ToString();
        StartDate = startDate;
        EndDate = endDate;
        Course = course;
        Students = students;
    }
    
    // Update constructor (takes existing ID)
    public CourseInstance(string id, DateTime startDate, DateTime endDate, Course course, List<Student> students)
    {
    
        CourseInstanceId = id;
        StartDate = startDate;
        EndDate = endDate;
        Course = course;
        Students = students;
    }
}