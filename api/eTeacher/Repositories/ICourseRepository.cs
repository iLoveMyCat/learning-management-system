using eTeacher.Models;

namespace eTeacher.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course GetById(Guid id);
        bool Add(Course course);
        bool Update(Course course);
        bool Delete(Guid id);
    }
}
