using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public interface IStudentsLogic
    {
        void UpdateCity(Students fan);

        public Students AddNewStudent(Students fan);
        public void DeleteStudent(int id);
        Students GetStudent(int id);
        IEnumerable<Students> GetAllStudents();

        List<KeyValuePair<int, int>> BestStudent();
        List<KeyValuePair<int, int>> WorstStudent();
        int ReservationsNumber(int id);

    }
}
