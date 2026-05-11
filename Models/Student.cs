//models/Student.cs
namespace SchoolApi.Models;

public class Student
{
    public string StudentId { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }

    // Default constructor for deserialization
    public Student()
    {
        StudentId = Guid.NewGuid().ToString()[..6];
        Name = string.Empty;
        Email = string.Empty;
    }

    
    // Constructor 1: for create (generates new ID) 
    public Student(string name, string email)
    {
        StudentId = Guid.NewGuid().ToString()[..6];
        Name = name;
        Email = email;
    }

    // Constructor 2: for update (receives existing ID)
    public Student(string id, string name, string email)
    {
        StudentId = id;
        Name = name;
        Email = email;
    }
}