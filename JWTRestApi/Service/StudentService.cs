using JWTRestApi.Models;

namespace JWTRestApi.Service
{
    public interface StudentService
    {
        public IEnumerable<Student> GetStudents();
        public IEnumerable<Studentresult> GetStudentresult(int Id);
        public Student GetStudent(int id);
            
        public void CreateStudent(Student student);

        public void UpdateStudent(int id, Student student);

        public void DeleteStudent(int id);
    }
}
