
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Context;
using SchoolApi.Models;
using SchoolApi.Repositories;
using SchoolApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// DATABASE (ONE DB ONLY)
// ---------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("SchoolDb"));

// ---------------------------
// IDENTITY
// ---------------------------
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ---------------------------
// SERVICES & REPOSITORIES
// ---------------------------

// SCOPED
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseInstanceRepository, CourseInstanceRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseInstanceService, CourseInstanceService>();
builder.Services.AddScoped<IGradeService, GradeService>();

// ---------------------------
// API
// ---------------------------
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ---------------------------
// DATABASE INIT
// ---------------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

// ---------------------------
// PIPELINE
// ---------------------------
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
