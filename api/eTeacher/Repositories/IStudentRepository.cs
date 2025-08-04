using eTeacher.Models;

namespace eTeacher.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student GetById(Guid id);
        bool Add(Student student);
        bool Update(Student student);
        bool Delete(Guid id);
    }

}
