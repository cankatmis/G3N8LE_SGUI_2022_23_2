using G3N8LE_ADT_2022_23_1.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace G3N8LE_ADT_2022_23_1.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NoncrudteacherController : ControllerBase
    {
        ITeachersLogic Tl;

        public NoncrudteacherController(ITeachersLogic tl)
        {
            Tl = tl;
        }


        // GET: Noncrudteacher/TeachersEarnings
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> TeachersEarnings()
        {
            return Tl.TeacherEarnings();
        }


        // GET: Noncrudteacher/MostPaidTeachers
        [HttpGet]
        public List<KeyValuePair<string, int>> MostPaidTeachers()
        {
            return Tl.MostPaidTeacher();
        }


        // GET: Noncrudteacher/LessPaidTeachers
        [HttpGet]
        public List<KeyValuePair<string, int>> LessPaidTeachers()
        {
            return Tl.LessPaidTeacher();
        }
    }
}
