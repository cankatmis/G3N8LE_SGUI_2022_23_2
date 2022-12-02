using G3N8LE_ADT_2022_23_1.Data;
using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Repository
{
    public class ReservationsRepository : Repository<Reservations>, IReservationsRepository
    {
        public ReservationsRepository(SchoolDbContext DbContext) : base(DbContext) { }
        public void UpdateDate(int id, DateTime newDate)
        {
            var reservation = this.GetOne(id);
            if (reservation == null)
            {
                throw new Exception("Unfortunately, this id doesn't match to any order in the database.");
            }
            else
            {
                reservation.DateTime = newDate;
                this.context.SaveChanges();
            }
        }
        public override Reservations GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(reservation => reservation.Id == id);
        }
    }
}
