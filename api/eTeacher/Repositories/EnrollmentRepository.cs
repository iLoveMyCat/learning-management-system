using eTeacher.Models;

namespace eTeacher.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly List<Enrollment> _enrollments = new();

        public IEnumerable<Enrollment> GetAll()
        {
            return _enrollments;
        }

        public bool Add(Enrollment enrollment)
        {
            if (enrollment == null) return false;
            _enrollments.Add(enrollment);
            return true;
        }
    }
}
