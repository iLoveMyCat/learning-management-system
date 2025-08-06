using eTeacher.DTOs.Students;
using eTeacher.Models;
using eTeacher.Repositories;
using eTeacher.Services.Interfaces;
using System.Text.Json;

namespace eTeacher.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAwsS3Reader _s3Reader; 

        public StudentService(IStudentRepository studentRepository, IAwsS3Reader s3Reader)
        {
            _studentRepository = studentRepository;
            _s3Reader = s3Reader;
        }

        //Downloads a students list file from S3 (mocked) and preloads it into the in-memory repo.
        //Accepts JSON array of { FirstName, LastName, Email }.
        private record SimpleStudent(string? FirstName, string? LastName, string? Email);
        public async Task<int> PreloadFromS3Async(string key, CancellationToken ct = default)
        {
            var text = await _s3Reader.DownloadTextAsync(key, ct);

            var items = JsonSerializer.Deserialize<List<SimpleStudent>>(text) ?? new();

            var inserted = 0;
            foreach (var i in items)
            {
                var student = new Student
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Email = i.Email
                };
                if (_studentRepository.Add(student)) inserted++;
            }
            return inserted;
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
