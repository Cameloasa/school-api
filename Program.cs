using Microsoft.AspNetCore.Identity;
using SchoolApi.Models;
using SchoolApi.Models.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// redirect HTTP to HTTPS
app.UseHttpsRedirection();

//hello world endpoint
app.MapGet("/hello", () => "Hello World!");

List<Student> students = [
        new ("John Doe", "john.doe@example.com"),
        new ("Jane Smith", "jane.smith@example.com"),
        new ("Alice Johnson", "alice.johnson@example.com"),
        new ("Bob Brown", "bob.brown@example.com"),
        new ("Charlie Davis", "charlie.davis@example.com")
    ];
//endpoint GET /students List of students
app.MapGet("/students", () => {
    try{
        if (students == null)
        {
            return Results.Ok(new List<Student>());
        }
        else
        {
            return Results.Ok(students);
        }
    }
    catch (Exception)
    {
        return Results.InternalServerError("An error occurred while retrieving the students.");
    }
});

//endpoint GET /students/{id} Student by id
app.MapGet("/students/{id}", (int id) =>{
    try{
        Student? found = students.FirstOrDefault(s => s.Id == id);
        if (found == null)
        {
            return Results.NotFound($"Student with ID {id} not found.");
        }
    
        return Results.Ok(found);   
    
    }catch (Exception){
        return Results.InternalServerError("An error occurred while retrieving the student.");
    }   
});

//endpoint POST /students Create a new student
app.MapPost("/students", (CreateStudentRequest request) =>{
    try{
        if (string.IsNullOrWhiteSpace(request.Name)||
            string.IsNullOrWhiteSpace(request.Email))
        {
            return Results.BadRequest("Invalid student data.");
        }

        Student newStudent =new ( request.Name, request.Email);
        students.Add(newStudent);
        return Results.Created($"/students", newStudent);
    }
    catch (Exception){
        return Results.InternalServerError("An error occurred while creating the student.");
    }
});

//endpoint PUT /students/{id} Update a student by id
app.MapPut("/students/{id}", (int id, CreateStudentRequest request) =>{
    try{
        // Find the student by ID
        Student? found = students.FirstOrDefault(s => s.Id == id);

        // If the student is not found, return a 404 Not Found response
        if (found == null)
        {
            return Results.NotFound($"Student with ID {id} not found.");
        }

        // validate the request data if not valid return a 400 Bad Request response
        if (
            string.IsNullOrWhiteSpace(request.Name)||
            string.IsNullOrWhiteSpace(request.Email))
        {
            return Results.BadRequest("Invalid student data.");
        }

        // Update the student properties Name and Email
        found.Name = request.Name;
        found.Email = request.Email;

        // Return the updated student
        return Results.Ok(found);
    }
    catch (Exception){
        return Results.InternalServerError("An error occurred while updating the student.");
    }
});

//endpoint DELETE /students/{id} Delete a student by id
app.MapDelete("/students/{id}", (int id) =>{
    try{
        // Find the student by ID
        Student? found = students.FirstOrDefault(s => s.Id == id);

        // If the student is not found, return a 404 Not Found response
        if (found == null)
        {
            return Results.NotFound($"Student with ID {id} not found.");
        }

        // Remove the student from the list
        students.Remove(found);

        // Return 204 No Content response -no body in the response
        return Results.NoContent();
    }
    catch (Exception){
        return Results.InternalServerError("An error occurred while deleting the student.");
    }
});

// List of courses
List<Course> courses = [
        new ("Mathematics", "An introduction to mathematical concepts and techniques."),
        new ("Physics", "A study of the fundamental principles governing the natural world."),
        new ("Chemistry", "An exploration of the properties and interactions of matter."),
        new ("Biology", "An examination of living organisms and their interactions with the environment."),
        new ("Computer Science", "A comprehensive overview of computer systems and programming."),
        new("History", "A study of past events and civilizations."),
        new("Geography", "An exploration of Earth's landscapes, environments, and populations."),
        new("Philosophy", "An introduction to fundamental questions about existence, knowledge, and ethics."),
        new("Economics", "A study of production, consumption, and distribution of resources."),
        new("Literature", "An analysis of written works across different periods and cultures."),
        new("Statistics", "An introduction to data analysis, probability, and statistical methods."),
        new("Software Engineering", "Principles and practices of designing and building software systems."),
        new("Databases", "Fundamentals of database design, SQL, and data management."),
        new("Cybersecurity", "An overview of protecting systems, networks, and data from digital attacks.")
    ];

// endpoint courses List of courses
app.MapGet("/courses", () =>{
    
    return Results.Ok(courses);
});

// endpoint course by id
app.MapGet("/courses/{id}", (int id) =>
{
    Course? course = null;

    for (int i = 0; i < courses.Count; i++)
    {
        if (courses[i].Id == id)
        {
            course = courses[i];
            break;
        }
    }

    if (course == null)
    {
        return Results.NotFound($"Course with ID {id} not found.");
    }

    return Results.Ok(course);
});

// endpoint POST /courses Create a new course
app.MapPost("/courses", (CreateCourseRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.Description))
    {
        return Results.BadRequest("Invalid course data.");
    }

    // validation to check if the course already exists by name (case-insensitive)
    var exists = courses.Any(c => 
        c.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase));

    if (exists)
    {
        return Results.Conflict("Course already exists.");
    }

    Course newCourse = new(request.Name, request.Description);
    courses.Add(newCourse);
    return Results.Created($"/courses/{newCourse.Id}", newCourse);
});

// endpoint PUT /courses/{id} Update a course by id
app.MapPut("/courses/{id}", (int id, CreateCourseRequest request) =>
{
    Course? found = courses.FirstOrDefault(c => c.Id == id);

    if (found == null)
    {
        return Results.NotFound($"Course with ID {id} not found.");
    }

    if (string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.Description))
    {
        return Results.BadRequest("Invalid course data.");
    }

    found.Name = request.Name;
    found.Description = request.Description;

    return Results.Ok(found);
});

// endpoint DELETE /courses/{id} Delete a course by id
app.MapDelete("/courses/{id}", (int id) =>
{
    Course? found = courses.FirstOrDefault(c => c.Id == id);

    if (found == null)
    {
        return Results.NotFound($"Course with ID {id} not found.");
    }

    courses.Remove(found);
    return Results.NoContent();
});


List<CourseInstance> courseInstances = [
    new ( DateTime.Now, DateTime.Now.AddMonths(3), courses[0], [ students[0], students[1] ]),
    new ( DateTime.Now, DateTime.Now.AddMonths(3), courses[1], [ students[2], students[3] ]),
    new ( DateTime.Now, DateTime.Now.AddMonths(3), courses[2], [ students[3], students[4] ]),
    new ( DateTime.Now, DateTime.Now.AddMonths(3), courses[3], [ students[0], students[2] ]),
    new ( DateTime.Now, DateTime.Now.AddMonths(3), courses[4], [ students[1], students[3] ])
];

// endpoint course-instances List of course instances
app.MapGet("/course-instances", () =>{
   
    return Results.Ok(courseInstances);
});

app.MapGet("/students/{studentId}/courses", (int studentId) =>
{
    var result = courseInstances
        .Where(ci => ci.Students.Any(s => s.Id == studentId))
        .Select(ci => ci.Course)
        .ToList();

    return result;
});

// endpoint course-instances/filter List of course instances filtered by start and end date
// example: /course-instances/filter?start=2024-01-01&end=2024-12-31
app.MapGet("/course-instances/filter", (DateTime start, DateTime end) =>
{
    var result = courseInstances
        .Where(ci => ci.StartDate >= start && ci.EndDate <= end)
        .ToList();

    return result;
});

// list of grades
List<Grade> grades = [
    new ( "A", courseInstances[0], students[0]),
    new ( "B", courseInstances[0], students[1]),
    new ( "A-", courseInstances[1], students[2]),
    new ( "B+", courseInstances[2], students[3]),
    new ( "A", courseInstances[2], students[4])
];

// endpoint grades List of grades
app.MapGet("/grades", () =>
{
   
    return Results.Ok(grades);
});


// endpoint grades by student id
app.MapGet("/students/{studentId}/grades", (int studentId) =>
{
    return grades.Where(g => g.Student.Id == studentId).ToList();
});

// endpoint grade by student id and course instance id
app.MapGet("/students/{studentId}/course-instances/{courseInstanceId}/grade", (int studentId, int courseInstanceId) =>
{
    var grade = grades.FirstOrDefault(g => g.Student.Id == studentId && g.CourseInstance.Id == courseInstanceId);
    return grade is null ? Results.NotFound() : Results.Ok(grade);
});

app.Run();


