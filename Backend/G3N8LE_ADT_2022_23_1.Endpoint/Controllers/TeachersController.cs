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
    public class TeachersController : ControllerBase
    {
        ITeachersLogic TL;
        IHubContext<SignalRHub> hub;
        public TeachersController(ITeachersLogic tL, IHubContext<SignalRHub> hub)
        {
            TL = tL;
            this.hub = hub;
        }

        // GET: /teachers
        [HttpGet]
        public IEnumerable<Teachers> Get()
        {
            return TL.GetAllTeachers();
        }


        // GET /teachers/5
        [HttpGet("{id}")]
        public Teachers Get(int id)
        {
            return TL.GetTeacher(id);
        }

        // POST /teachers
        [HttpPost]
        public void Post([FromBody] Teachers value)
        {
            TL.AddNewTeacher(value);
            this.hub.Clients.All.SendAsync("TeacherCreated", value);
        }


        // PUT /teachers
        [HttpPut]
        public void Put([FromBody] Teachers value)
        {
            TL.UpdateTeacherCost(value);
            this.hub.Clients.All.SendAsync("TeacherUpdated", value);
        }


        // DELETE /teachers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var teacherToDelete = this.TL.GetTeacher(id);
            TL.DeleteTeacher(id);
            this.hub.Clients.All.SendAsync("TeacherDeleted", teacherToDelete);
        }

    }
}
