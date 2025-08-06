using eTeacher.DTOs.Enrollments;
using eTeacher.Models;
using eTeacher.Repositories;
using eTeacher.Services.Interfaces;

namespace eTeacher.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public IEnumerable<EnrollmentReadDto> GetAllEnrollments()
        {
            var enrollments = _enrollmentRepository.GetAll();

            return enrollments.Select(e => new EnrollmentReadDto
            {
                Id = e.Id,
                StudentId = e.StudentId,
                StudentFirstName = _studentRepository.GetById(e.StudentId)?.FirstName,
                StudentLastName = _studentRepository.GetById(e.StudentId)?.LastName,
                CourseId = e.CourseId,
                CourseTitle = _courseRepository.GetById(e.CourseId)?.Title
            });
        }

        public EnrollmentReadDto EnrollStudent(EnrollmentCreateDto dto)
        {
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId
            };

            _enrollmentRepository.Add(enrollment);

            return new EnrollmentReadDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentFirstName = _studentRepository.GetById(dto.StudentId)?.FirstName,
                StudentLastName = _studentRepository.GetById(dto.StudentId)?.LastName,
                CourseId = enrollment.CourseId,
                CourseTitle = _courseRepository.GetById(dto.CourseId)?.Title
            };
        }
        public IEnumerable<EnrollmentReportDto> GetEnrollmentReport()
    {
            var enrollments = _enrollmentRepository.GetAll();

            // Group enrollments by courseId
            var grouped = enrollments
                .GroupBy(e => e.CourseId)
                .Select(group =>
                {
                    var course = _courseRepository.GetById(group.Key); 
                    return new EnrollmentReportDto
                    {
                        CourseTitle = course?.Title ?? "Unknown",
                        TotalStudents = group.Count()
                    };
                });

            return grouped;
        }
    }
}
