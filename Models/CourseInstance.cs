namespace SchoolApi.Models;

    public class CourseInstance
{
    private static int _counter = 1;
    
    public int Id { get; private set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Course Course { get; set; }
    public List<Student> Students { get; set; }

    public CourseInstance(DateTime startDate, DateTime endDate, Course course, List<Student> students)
    {
        Id = _counter++;
    
    // Date validation (StartDate and EndDate are required)
    if (startDate == default || endDate == default)
    {
        throw new ArgumentException("Start date and end date are required.");
    }

    // Validate that StartDate is before EndDate
    if (startDate >= endDate)
    {
        throw new ArgumentException("Start date must be before end date.");
    }

    StartDate = startDate;
    EndDate = endDate;
    Course = course;
    Students = students;
    }
}