using eTeacher.DTOs.Enrollments;

namespace eTeacher.Services.Interfaces
{
    public interface IEnrollmentService
    {
        IEnumerable<EnrollmentReadDto> GetAllEnrollments();
        EnrollmentReadDto EnrollStudent(EnrollmentCreateDto dto);
        public IEnumerable<EnrollmentReportDto> GetEnrollmentReport();
    }

}
