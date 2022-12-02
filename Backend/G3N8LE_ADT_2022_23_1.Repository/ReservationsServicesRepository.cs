using G3N8LE_ADT_2022_23_1.Data;
using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Repository
{
    public class ReservationsServicesRepository : Repository<ReservationsServices>, IReservationsServicesRepository
    {
        public ReservationsServicesRepository(SchoolDbContext DbContext) : base(DbContext) { }
        public override ReservationsServices GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(connection => connection.Id == id);
        }
    }
}
