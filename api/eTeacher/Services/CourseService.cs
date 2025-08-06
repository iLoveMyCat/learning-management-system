using eTeacher.DTOs.Courses;
using eTeacher.Models;
using eTeacher.Repositories;
using eTeacher.Services.Interfaces;

namespace eTeacher.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<CourseReadDto> GetAllCourses()
        {
            return _courseRepository.GetAll().Select(c => new CourseReadDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
            });
        }

        public CourseReadDto GetCourseById(Guid id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null) return null;

            return new CourseReadDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
            };
        }

        public CourseReadDto CreateCourse(CourseCreateDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
            };

            _courseRepository.Add(course);

            return new CourseReadDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
            };
        }

        public bool UpdateCourse(Guid id, CourseUpdateDto dto)
        {
            var courseToUpdate = new Course
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
            };

            return _courseRepository.Update(courseToUpdate);
        }

        public bool DeleteCourse(Guid id)
        {
            return _courseRepository.Delete(id);
        }
    }
}
