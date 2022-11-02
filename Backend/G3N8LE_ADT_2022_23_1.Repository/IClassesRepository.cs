using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Repository
{
    public interface IClassesRepository : IRepository<Classes>
    {
        void UpdatePrice(int id, int newPrice);
    }
}
