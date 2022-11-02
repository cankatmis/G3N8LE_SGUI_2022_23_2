using G3N8LE_ADT_2022_23_1.Data;
using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Repository
{
    public class TeachersRepository : Repository<Teachers>, ITeachersRepository
    {
        public TeachersRepository(XYZDbContext DbContext) : base(DbContext) { }
        public override Teachers GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(teacher => teacher.Id == id);

        }
        public void UpdatePrice(int id, int newprice)
        {
            var teacher = this.GetOne(id);
            if (teacher == null)
            {
                throw new Exception("Unfortunately, this id doesn't match to any teachers in the database.");
            }
            else
            {
                teacher.Price = newprice;
                this.context.SaveChanges();
            }
        }
    }
}
