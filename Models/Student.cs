//models/Student.cs
namespace SchoolApi.Models;

public class Student ( string name, string email){

    
    public string StudentId {get;} = Guid.NewGuid().ToString();
    public string Name {get; set;} = name;
    public string Email {get; set;} = email;
}
