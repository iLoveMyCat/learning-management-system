using eTeacher.DTOs.Students;
using eTeacher.Models;
using eTeacher.Repositories;

namespace eTeacher.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IEnumerable<StudentReadDto> GetAllStudents()
        {
            return _studentRepository.GetAll().Select(s => new StudentReadDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email
            });
        }

        public StudentReadDto GetStudentById(Guid id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null) return null;

            return new StudentReadDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            };
        }

        public StudentReadDto CreateStudent(StudentCreateDto dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            _studentRepository.Add(student);

            return new StudentReadDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            };
        }
        public bool UpdateStudent(StudentUpdateDto dto)
        {
            var student = new Student
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            return _studentRepository.Update(student);
        }

        public bool DeleteStudent(Guid id)
        {
            return _studentRepository.Delete(id);
        }
    }
}
