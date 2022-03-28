using Microsoft.AspNetCore.Mvc;
using JWTRestApi.Models;
using JWTRestApi.Service;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTRestApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase

    {
            private  readonly StudentService _studentService;      
        public StudentController(StudentService studentService)
            {
            
            _studentService = studentService;
            
            }
        // GET: api/<StudentController>
        [HttpGet("GetStudent"),Authorize]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
           
            return _studentService.GetStudents().ToList();
        }
     
        [HttpGet("GetStudentresult")]
        public ActionResult<IEnumerable<Studentresult>> GetStudentsreult(int Id)
        {

            return _studentService.GetStudentresult(Id).ToList();
        }
        
        [HttpPost("insertStudent")]
        public ActionResult insertStudent(Student student)
        {
            _studentService.CreateStudent(student);

            return Ok(student);
        }
    }
}
