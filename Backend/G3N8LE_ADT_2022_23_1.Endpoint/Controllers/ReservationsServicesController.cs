using G3N8LE_ADT_2022_23_1.Logic;
using G3N8LE_ADT_2022_23_1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace G3N8LE_ADT_2022_23_1.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationsServicesController : ControllerBase
    {
        IReservationsServicesLogic RSL;

        public ReservationsServicesController(IReservationsServicesLogic rSL)
        {
            RSL = rSL;
        }

        // GET: /reservationsservices
        [HttpGet]
        public IEnumerable<ReservationsServices> Get()
        {
            return RSL.GetAllConnections();
        }

        // GET /reservationsservices/5
        [HttpGet("{id}")]
        public ReservationsServices Get(int id)
        {
            return RSL.GetConnection(id);
        }

        // POST /reservationsservices
        [HttpPost]
        public void Post([FromBody] ReservationsServices value)
        {
            RSL.AddNewConnection(value);
        }

        // DELETE /reservationsservices/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RSL.DeleteConnection(id);
        }
    }
}
