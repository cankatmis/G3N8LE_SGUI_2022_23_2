using G3N8LE_ADT_2022_23_1.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace G3N8LE_ADT_2022_23_1.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NoncrudstudentController : ControllerBase
    {
        IStudentsLogic SL;

        public NoncrudstudentController(IStudentsLogic sL)
        {
            SL = sL;
        }

        // GET: Noncrudstudent/ReservationNUM/id
        [HttpGet("{id}")]
        public int ReservationNUM(int id)
        {
            return SL.ReservationsNumber(id);
        }

        // GET: Noncrudstudent/BestStudents
        [HttpGet]
        public List<KeyValuePair<int, int>> BestStudents()
        {
            return SL.BestStudent();
        }

        // GET: Noncrudstudent/WorstStudents
        [HttpGet]
        public List<KeyValuePair<int, int>> WorstStudents()
        {
            return SL.WorstStudent();
        }
    }
}
