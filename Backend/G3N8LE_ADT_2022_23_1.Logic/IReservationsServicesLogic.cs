using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public interface IReservationsServicesLogic
    {
        public ReservationsServices AddNewConnection(ReservationsServices reservservice);
        public void DeleteConnection(int id);
        public ReservationsServices GetConnection(int id);
        public IEnumerable<ReservationsServices> GetAllConnections();
    }
}
