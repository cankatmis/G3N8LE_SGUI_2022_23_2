using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public interface IReservationsLogic
    {
        void UpdateReservationDate(Reservations reser);
        public Reservations AddNewReservation(Reservations reser);
        public void DeleteReservation(int id);
        Reservations GetReservation(int id);
        IEnumerable<Reservations> GetAllReservations();
    }
}
