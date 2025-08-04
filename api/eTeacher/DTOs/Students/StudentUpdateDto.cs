namespace eTeacher.DTOs.Students
{
    public class StudentUpdateDto
    {
        public Guid Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
