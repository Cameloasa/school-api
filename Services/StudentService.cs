namespace SchoolApi.Services;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

public interface IStudentService{
    List<Student> GetStudents();
    Student? GetStudentById(string id);
    Student CreateStudent(CreateStudentRequest request);
    Student? UpdateStudent(string id, CreateStudentRequest request);
    bool DeleteStudent(string id);
}

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository repo)
    {
        _studentRepository = repo;
    }
    
    //get all students
    public List<Student> GetStudents()
    {
        try
        {
            return _studentRepository.GetAllStudents().ToList();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while retrieving students", ex);
        }
    }

    //get student by id
    public Student? GetStudentById(string id)
    {
        try
        {
            // Validation
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Student ID cannot be empty");
            }
            
            return _studentRepository.GetStudentById(id);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while retrieving student with ID {id}", ex);
        }
    }

    //create student
    public Student CreateStudent(CreateStudentRequest request)
    {
        try
        {
            // Validations
            if(_studentRepository.EmailExists(request.Email))
            {
                throw new ArgumentException("Email already exists");
            }
            
            Student newStudent = new(request.Name, request.Email);
            bool success = _studentRepository.AddStudent(newStudent);
            
            if (!success)
            {
                throw new InvalidOperationException("Failed to create student");
            }
            
            return newStudent;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while creating the student", ex);
        }
    }

    //update student
    public Student? UpdateStudent(string id, CreateStudentRequest request)
    {
        try
        {
            // validations
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Student ID cannot be empty");
            }
            
            if (request.Equals(default(CreateStudentRequest)))
            {
                throw new ArgumentException("Request cannot be null");
            }
            
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Name is required");
            }
            
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email is required");
            }
            
            // Find existing student
            var existingStudent = _studentRepository.GetStudentById(id);
            if (existingStudent == null)
            {
                return null;
            }
            
            // Update properties
            existingStudent.Name = request.Name;
            existingStudent.Email = request.Email;
            
            // Save to repository
            var updatedStudent = _studentRepository.UpdateStudent(existingStudent);
            return updatedStudent;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while updating student with ID {id}", ex);
        }
    }

    //delete student
    public bool DeleteStudent(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Student ID cannot be empty");
            }
            
            return _studentRepository.DeleteStudent(id);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while deleting student with ID {id}", ex);
        }
    }
}