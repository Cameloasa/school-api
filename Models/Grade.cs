namespace SchoolApi.Models;
public class Grade(string value, 
                    CourseInstance courseInstance, 
                    Student student)
{
    private static int _counter = 1;
    public int Id { get; private set; } = _counter++;
    public string Value { get; set; } = value;
    public CourseInstance CourseInstance { get; set; } = courseInstance;
    public Student Student { get; set; } = student;
}
