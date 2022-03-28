using JWTRestApi.Models;
using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace JWTRestApi.Service
{
    public class StudentCommonService : StudentService

    {
        private List<Student> _students;
        private List<Studentresult> _studentresult;

           public static String connString = "server=localhost;user id=root;password=iiot;port=3306;database=CollegeDB;";
           public MySqlConnection conn = new MySqlConnection( connString);

        public StudentCommonService()
        {
        }

        public void CreateStudent(Student student)
        {
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                var name = student.Name;
                var uniqueCode = student.UniqueCode;
                var section = student.Section;

                string sql = "INSERT INTO student (Name, uniqueCode, Section) VALUES ('" + name + "','" + uniqueCode + "','" + section + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int res =  cmd.ExecuteNonQuery();
                Console.WriteLine("Affect Rows " + res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
        }

        public void DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }


        public Student GetStudent(int id)
        {

            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudents()
        {
            List<Student> studentList = new List<Student>();
            conn.Open();
            
            //Console.WriteLine("instance Check"+ FakeGuid);


            MySqlCommand cmd = new MySqlCommand("SELECT Id,Name,uniqueCode,Section FROM student", conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Student m = new Student
                {
                    Id = (int)rdr[0],
                    Name = (string)rdr[1],
                    UniqueCode = (string)rdr[2],
                    Section = (string)rdr[3],
                    Guid = (Guid)Guid.NewGuid(),
                };
                studentList.Add(m);
            }
            conn.Close();
            _students = studentList;
            return _students;
        }
        public IEnumerable<Studentresult> GetStudentresult(int Id)
        {
            List<Studentresult> reslutList = new List<Studentresult>();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT Id,Semester,Subject,Mark,student_Id FROM Results where student_Id = "+Id, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Studentresult m = new Studentresult
                {
                    Id = (int)rdr[0],
                    Semmester = (int)rdr[1],
                    Subject = (string)rdr[2],
                    Mark = (int)rdr[3],
                    StudentID = (int)rdr[4],
                };
                reslutList.Add(m);
            }
            conn.Close();
            _studentresult = reslutList;
            return _studentresult;
        }

        public void UpdateStudent(int id, Student student)
        {
            throw new NotImplementedException();
        }

    }
}
