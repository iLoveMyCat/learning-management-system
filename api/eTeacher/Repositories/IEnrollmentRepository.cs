using eTeacher.Models;

namespace eTeacher.Repositories
{
    public interface IEnrollmentRepository
    {
        IEnumerable<Enrollment> GetAll();
        bool Add(Enrollment enrollment);
    }
}
