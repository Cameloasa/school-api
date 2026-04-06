namespace SchoolApi.Models;

    public class CourseInstance(
                            DateTime startDate, 
                            DateTime endDate, 
                            Course course, 
                            List<Student> students){

        private static int _counter = 1;
        public int Id { get; private set; } = _counter++;
        public DateTime StartDate { get; set; } = startDate;
        public DateTime EndDate { get; set; } = endDate;
        public Course Course { get; set; } = course;
        public List<Student> Students { get; set; } = students;
    }
