using G3N8LE_ADT_2022_23_1.Data;
using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Repository
{
    public class StudentsRepository : Repository<Students>, IStudentsRepository
    {
        public StudentsRepository(SchoolDbContext DbContext) : base(DbContext) { }
        public override Students GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(student => student.Id == id);

        }
        public void UpdateCity(int id, string newCity)
        {
            var student = this.GetOne(id);
            if (student == null)
            {
                throw new Exception("Unfortunately, this id doesn't match to any of the students in the database.");
            }
            else
            {
                student.City = newCity;
                this.context.SaveChanges();
            }
        }
    }
}
