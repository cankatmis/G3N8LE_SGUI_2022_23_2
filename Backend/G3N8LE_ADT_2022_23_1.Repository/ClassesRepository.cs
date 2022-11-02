using G3N8LE_ADT_2022_23_1.Data;
using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Repository
{
    public class ClassesRepository : Repository<Classes>, IClassesRepository
    {
        public ClassesRepository(XYZDbContext DbContext) : base(DbContext) { }
        public override Classes GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(class1 => class1.Id == id);
        }

        public void UpdatePrice(int id, int newPrice)
        {
            var class1 = this.GetOne(id);
            if (class1 == null)
            {
                throw new Exception("Unfortunately, this id doesn't match to any service in the database.");
            }
            else
            {
                class1.Price = newPrice;
                this.context.SaveChanges();
            }
        }
    }
}
