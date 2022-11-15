using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public interface ITeachersLogic
    {
        public Teachers AddNewTeacher(Teachers newTeacher);
        public void DeleteTeacher(int id);
        Teachers GetTeacher(int id);
        IEnumerable<Teachers> GetAllTeachers();
        void UpdateTeacherCost(Teachers value);

        IEnumerable<KeyValuePair<string, int>> TeacherEarnings();
        List<KeyValuePair<string, int>> MostPaidTeacher();
        List<KeyValuePair<string, int>> LessPaidTeacher();
    }
}
