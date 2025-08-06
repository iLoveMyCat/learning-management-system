using eTeacher.DTOs.Courses;

namespace eTeacher.Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseReadDto> GetAllCourses();
        CourseReadDto GetCourseById(Guid id);
        CourseReadDto CreateCourse(CourseCreateDto dto);
        bool UpdateCourse(Guid id, CourseUpdateDto dto);
        bool DeleteCourse(Guid id);
    }
}
