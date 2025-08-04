namespace eTeacher.DTOs.Enrollments
{
    public class EnrollmentReadDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; }
    }
}
