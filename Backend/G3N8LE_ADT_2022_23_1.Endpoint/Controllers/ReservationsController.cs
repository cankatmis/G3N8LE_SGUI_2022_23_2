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
    public class ReservationsController : ControllerBase
    {
        IReservationsLogic RL;
        IHubContext<SignalRHub> hub;
        public ReservationsController(IReservationsLogic rL, IHubContext<SignalRHub> hub)
        {
            RL = rL;
            this.hub = hub;
        }

        // GET: /reservations
        [HttpGet]
        public IEnumerable<Reservations> Get()
        {
            return RL.GetAllReservations();
        }

        // GET /resevations/5
        [HttpGet("{id}")]
        public Reservations Get(int id)
        {
            return RL.GetReservation(id);
        }

        // POST /reservations
        [HttpPost]
        public void Post([FromBody] Reservations value)
        {
            RL.AddNewReservation(value);
            this.hub.Clients.All.SendAsync("ReservationCreated", value);
        }


        // PUT /reservations
        [HttpPut]
        public void Put([FromBody] Reservations value)
        {
            RL.UpdateReservationDate(value);
            this.hub.Clients.All.SendAsync("ReservationUpdated", value);
        }


        // DELETE /reservations/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var reservationToDelete = this.RL.GetReservation(id);
            RL.DeleteReservation(id);
            this.hub.Clients.All.SendAsync("ReservationDeleted", reservationToDelete);
        }
    }
}
