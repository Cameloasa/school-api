namespace SchoolApi.Models;

public class Course{
    
    public string CourseId {get; set;} 
    public string Name { get; set; } 
    public string Description { get; set; } 

    // Default constructor for deserialization
    public Course()
    {
        CourseId = Guid.NewGuid().ToString();
        Name = string.Empty;
        Description = string.Empty;
    }
    
    // Constructor 1: for create (generates new ID)
    public Course(string name, string description)
    {
        CourseId = Guid.NewGuid().ToString();
        Name = name;
        Description = description;
    } 
    // Constructor 2: for update (receives existing ID)
    public Course(string id, string name, string description)
    {
        CourseId = id;
        Name = name;
        Description = description;
    }

   
}
