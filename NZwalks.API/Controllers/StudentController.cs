using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZwalks.API.Controllers
{
    //https://localhost:portnumber/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase

    {   //Get:https://localhost:portnumber/api/Students
        [HttpGet]
        public IActionResult GetallStudents()
        {
            var Studentslist = new String[] { "Harsh", "Divyansh", "Atul", "Aditya", "Dhruv" };
            return Ok(Studentslist);
        }
    }
}
