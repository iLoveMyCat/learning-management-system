using eTeacher.Models;

namespace eTeacher.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly List<Course> _courses = new();

        public IEnumerable<Course> GetAll()
        {
            return _courses;
        }
        public Course GetById(Guid id)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }

        public bool Add(Course course)
        {
            if (course == null) return false;
            _courses.Add(course);
            return true;
        }

        public bool Update(Course course)
        {
            var existing = GetById(course.Id);
            if (existing == null) return false;

            existing.Title = course.Title;
            existing.Description = course.Description;
            return true;
        }

        public bool Delete(Guid id)
        {
            var course = GetById(id);
            return course != null && _courses.Remove(course);
        }
    }
}
