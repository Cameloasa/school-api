namespace SchoolApi.Models;

public class Course ( string name, string description){
    private static int _counter = 1;
    public int CourseId {get; private set;} = _counter++;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}
