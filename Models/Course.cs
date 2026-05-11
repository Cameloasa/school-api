namespace SchoolApi.Models;

public class Course{
    
    public string CourseId {get; set;} 
    public string Title { get; set; } 
    public string Description { get; set; } 

    // Default constructor for deserialization
    public Course()
    {
        CourseId = Guid.NewGuid().ToString()[..6];
        Title = string.Empty;
        Description = string.Empty;
    }
    
    // Constructor 1: for create (generates new ID)
    public Course(string title, string description)
    {
        CourseId = Guid.NewGuid().ToString()[..6];
        Title = title;
        Description = description;
    } 
    // Constructor 2: for update (receives existing ID)
    public Course(string id, string title, string description)
    {
        CourseId = id;
        Title = title;
        Description = description;
    }

   
}
