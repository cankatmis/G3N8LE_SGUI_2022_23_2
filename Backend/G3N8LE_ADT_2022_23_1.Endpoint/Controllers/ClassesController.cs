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
    public class ClassesController : ControllerBase
    {
        IClassesLogic CL;
        IHubContext<SignalRHub> hub;

        public ClassesController(IClassesLogic cL, IHubContext<SignalRHub> hub)
        {
            CL = cL;
            this.hub = hub;
        }

        // GET: /classes
        [HttpGet]
        public IEnumerable<Classes> Get()
        {
            return CL.GetAllClasses();
        }


        // GET /classes/5
        [HttpGet("{id}")]
        public Classes Get(int id)
        {
            return CL.GetClass(id);
        }

        // POST /classes
        [HttpPost]
        public void Post([FromBody] Classes value)
        {
            CL.AddNewClass(value);
            this.hub.Clients.All.SendAsync("ClassCreated", value);
        }


        // PUT /classes
        [HttpPut]
        public void Put([FromBody] Classes value)
        {
            CL.UpdateClassCost(value);
            this.hub.Clients.All.SendAsync("ClassUpdated", value);
        }


        // DELETE /classes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var classToDelete = this.CL.GetClass(id);
            CL.DeleteClass(id);
            this.hub.Clients.All.SendAsync("ClassDeleted", classToDelete);
        }
    }
}
