namespace eTeacher.Models
{
    public class Enrollment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
