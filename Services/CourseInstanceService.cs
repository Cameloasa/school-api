
using SchoolApi.Mappers;
using SchoolApi.Models;
using SchoolApi.Models.DTOs;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;
using SchoolApi.Validators;

namespace SchoolApi.Services;

// =========================
//      INTERFACE
// =========================
public interface ICourseInstanceService
{
    Task<List<CourseInstanceResponse>> GetInstancesAsync();
    Task<CourseInstanceResponse?> GetInstanceByIdAsync(string id);
    Task<List<CourseInstanceResponse>> GetInstancesByCourseIdAsync(string courseId);

    Task<CourseInstanceResponse> CreateInstanceAsync(CreateCourseInstanceRequest request);
    Task<CourseInstanceResponse?> UpdateInstanceAsync(string id, UpdateCourseInstanceRequest request);
    Task<bool> DeleteInstanceAsync(string id);
}
// =========================
//      IMPLEMENTATION
// =========================
public class CourseInstanceService : ICourseInstanceService
{
    private readonly ICourseInstanceRepository _courseInstanceRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;

    public CourseInstanceService(
        ICourseInstanceRepository instanceRepo,
        IStudentRepository studentRepo,
        ICourseRepository courseRepo)
    {
        _courseInstanceRepository = instanceRepo;
        _studentRepository = studentRepo;
        _courseRepository = courseRepo;
    }

    // create
    public async Task<CourseInstanceResponse> CreateInstanceAsync(CreateCourseInstanceRequest request)
    {
        var existingInstance = await _courseInstanceRepository.GetByCourseAndDatesAsync(
            request.CourseId,
            request.StartDate,
            request.EndDate
        );

        if (existingInstance != null)
        throw new Exception("This course already has an instance in the same date range.");
        // 1. course validation
        var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
        if (course == null)
            throw new Exception("Course not found");

        // 2. students
        var students = await _studentRepository.GetStudentsByIdsAsync(request.StudentIds);

        if (students.Count != request.StudentIds.Count)
            throw new Exception("One or more students do not exist");

        // 3. create instance 
        var instance = new CourseInstance
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CourseId = request.CourseId,
            Students = students 
        };

        // 4. save
        var created = await _courseInstanceRepository.AddInstanceAsync(instance);

        // 5. return DTO
        return CourseInstanceMapper.ToResponse(created);
    }

    //delete
    public async Task<bool> DeleteInstanceAsync(string id)
    {
        return await _courseInstanceRepository.DeleteInstanceAsync(id);
    }

    //get by id
    public async Task<CourseInstanceResponse?> GetInstanceByIdAsync(string id)
    {
        var instance = await _courseInstanceRepository.GetInstanceByIdAsync(id);
        return instance is null? null: CourseInstanceMapper.ToResponse(instance);
    }

    //get all
    public async Task<List<CourseInstanceResponse>> GetInstancesAsync()
    {
        var instances = await _courseInstanceRepository.GetInstancesAsync();
        return instances.Select(CourseInstanceMapper.ToResponse).ToList();
    }

    //get by course id
    public async Task<List<CourseInstanceResponse>> GetInstancesByCourseIdAsync(string courseId)
    {
        var instances = await _courseInstanceRepository.GetInstancesByCourseIdAsync(courseId);

        if (instances == null || instances.Count == 0)
            return new List<CourseInstanceResponse>();

        return instances
            .Select(CourseInstanceMapper.ToResponse)
            .ToList();
    }

    //update
    public async Task<CourseInstanceResponse?> UpdateInstanceAsync(string id, UpdateCourseInstanceRequest request)
    {
    
        // 1. instance from db
        var existing = await _courseInstanceRepository.GetInstanceByIdAsync(id);
        if (existing == null)
            return null;

        // 2. date validation
        var validator = new DateValidation(request.StartDate, request.EndDate);
        var errors = validator.Validate();

        if (errors.Any())
            throw new Exception(string.Join(" | ", errors));

        // 3. modify 
        existing.StartDate = request.StartDate;
        existing.EndDate = request.EndDate;

        // 4. send to repo
        var updated = await _courseInstanceRepository.UpdateInstanceAsync(existing);

        // 5. Return DTO
        return CourseInstanceMapper.ToResponse(updated!);
    }

}