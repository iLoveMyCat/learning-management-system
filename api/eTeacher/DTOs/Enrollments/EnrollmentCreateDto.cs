namespace eTeacher.DTOs.Enrollments
{
    public class EnrollmentCreateDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
