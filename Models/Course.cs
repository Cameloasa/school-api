namespace SchoolApi.Models;

public class Course ( string name, string description){
    
    public string CourseId {get;} = Guid.NewGuid().ToString();
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}
