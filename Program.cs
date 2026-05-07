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
builder.Services.AddSingleton<ICourseInstanceService, CourseInstanceService>();
builder.Services.AddSingleton<IGradeService, GradeService>();

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

app.Run();


