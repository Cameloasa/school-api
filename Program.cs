using Microsoft.AspNetCore.Identity;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<ICourseService, CourseService>();

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

app.MapControllers();



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


