# School API

This is a simple .NET Web API project that models a school system with students, courses, course instances, and grades. It provides endpoints to retrieve information about students, courses, course sessions, and grades.

## 🧩 Features

List all students
List all courses and get course by ID
List all course instances (course sessions)
Filter course instances by student or by date range
List all grades and filter grades by student or by course session

## 📦 Models

1. Student
Id (int)
Name (string)
Email (string)
2. Course
Id (int)
Title (string)
Description (string)
3. CourseInstance
Id (int)
StartDate (DateTime)
EndDate (DateTime)
Course (Course)
Students (List<Student>)
4. Grade
Id (int)
Value (string, e.g., "A", "B+", "C-")
CourseInstance (CourseInstance)
Student (Student)

## 🚀 Endpoints

1. Students
GET /students – List all students
GET /students/{id} -Get a student by Id
POST /students - Create a student
PUT /students/{id} - Modify name or email for a student
DELETE /students/{id} - Delete a student by id

2. Courses
GET /courses – List all courses
GET /courses/{id} – Get a specific course by ID

3. Course Instances
GET /course-instances – List all course sessions
GET /students/{studentId}/courses – Get all courses for a specific student
GET /course-instances/filter?start={start}&end={end} – Filter course sessions by date range

4. Grades
GET /grades – List all grades
GET /students/{studentId}/grades – List all grades for a specific student
GET /students/{studentId}/course-instances/{courseInstanceId}/grade – Get the grade for a student in a specific course session

## 🛠 How to Run

Make sure you have .NET 10 installed.
Clone the repository:
git clone <repository-url>
Navigate to the project folder and run:
dotnet run
Open your browser or Postman and test the endpoints, for example:
[https://localhost:5001/students]

## 💡 Notes

This project uses in-memory lists for all data, so all data is reset every time the application restarts.
Designed for learning purposes to practice C# classes, lists, LINQ, and building simple REST APIs.
Endpoints return JSON objects that include nested data (e.g., course instances include course and students).
