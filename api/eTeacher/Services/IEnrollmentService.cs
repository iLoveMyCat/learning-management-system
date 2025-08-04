using eTeacher.DTOs.Enrollments;

namespace eTeacher.Services
{
    public interface IEnrollmentService
    {
        IEnumerable<EnrollmentReadDto> GetAllEnrollments();
        EnrollmentReadDto EnrollStudent(EnrollmentCreateDto dto);
    }

}
