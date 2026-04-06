using Microsoft.AspNetCore.Authentication;

namespace SchoolApi.Models;

public class Student ( string name, string email){

     private static int _counter = 1;
    public int Id {get; private set;} = _counter++;
    public string Name {get; set;} = name;
    public string Email {get; set;} = email;
}
