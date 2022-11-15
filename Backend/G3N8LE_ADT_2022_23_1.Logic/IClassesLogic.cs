using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public interface IClassesLogic
    {
        Classes GetClass(int id);
        IEnumerable<Classes> GetAllClasses();
        void UpdateClassCost(Classes clas);
        public Classes AddNewClass(Classes clas);
        public void DeleteClass(int id);
    }
}
