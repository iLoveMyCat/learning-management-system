using eTeacher.DTOs.Students;

namespace eTeacher.Services.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentReadDto> GetAllStudents();
        StudentReadDto GetStudentById(Guid id);
        StudentReadDto CreateStudent(StudentCreateDto dto);
        bool UpdateStudent(StudentUpdateDto dto);
        bool DeleteStudent(Guid id);
    }
}
