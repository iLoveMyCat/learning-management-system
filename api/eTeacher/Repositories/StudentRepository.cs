using eTeacher.Models;

namespace eTeacher.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student GetById(Guid id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public bool Add(Student student)
        {
            if (student == null) return false;
            _students.Add(student);
            return true;
        }

        public bool Delete(Guid id)
        {
            var student = GetById(id);
            return student != null && _students.Remove(student);
        }

        public bool Update(Student student)
        {
            var existing = GetById(student.Id);
            if (existing == null) return false;

            existing.FirstName = student.FirstName;
            existing.LastName = student.LastName;
            existing.Email = student.Email;
            return true;
        }
    }
}
