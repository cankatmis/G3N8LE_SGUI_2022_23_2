using G3N8LE_ADT_2022_23_1.Endpoint.Services;
using G3N8LE_ADT_2022_23_1.Logic;
using G3N8LE_ADT_2022_23_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace G3N8LE_ADT_2022_23_1.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        IStudentsLogic SL;
        IHubContext<SignalRHub> hub;
        public StudentsController(IStudentsLogic sL, IHubContext<SignalRHub> hub)
        {
            SL = sL;
            this.hub = hub;
        }
        // GET: /Students
        [HttpGet]
        public IEnumerable<Students> Get()
        {
            return SL.GetAllStudents();
        }


        // GET /students/5
        [HttpGet("{id}")]
        public Students Get(int id)
        {
            return SL.GetStudent(id);
        }

        // POST /students
        [HttpPost]
        public void Post([FromBody] Students value)
        {
            SL.AddNewStudent(value);
            this.hub.Clients.All.SendAsync("StudentCreated", value);
        }


        // PUT /students
        [HttpPut]
        public void Put([FromBody] Students value)
        {
            SL.UpdateCity(value);
            this.hub.Clients.All.SendAsync("StudentUpdated", value);

        }


        // DELETE /students/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var studentToDelete = this.SL.GetStudent(id);
            SL.DeleteStudent(id);
            this.hub.Clients.All.SendAsync("StudentDeleted", studentToDelete);
        }
    }
}
