using G3N8LE_ADT_2022_23_1.Models;
using G3N8LE_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public class TeachersLogic : ITeachersLogic
    {
        protected ITeachersRepository _TeacherRepository;
        protected IReservationsRepository _ReservationsRepository;

        public TeachersLogic(ITeachersRepository teacherRepository, IReservationsRepository reservationsRepository)
        {
            _TeacherRepository = teacherRepository;
            _ReservationsRepository = reservationsRepository;
        }

        public Teachers AddNewTeacher(Teachers NewTeacher)
        {

            this._TeacherRepository.Add(NewTeacher);
            return NewTeacher;
        }
        public void DeleteTeacher(int id)
        {
            Teachers TeacherToDelete = this._TeacherRepository.GetOne(id);
            if (TeacherToDelete != null)
            {
                this._TeacherRepository.Delete(TeacherToDelete);
            }
            else
            {
                throw new ArgumentException("This ID can't be found in the Teachers' database.");
            }
        }
        public void UpdateTeacherCost(Teachers value)
        {
            this._TeacherRepository.UpdatePrice(value.Id, value.Price);
        }
        public IEnumerable<Teachers> GetAllTeachers()
        {
            return this._TeacherRepository.GetAll();
        }
        public Teachers GetTeacher(int id)
        {
            Teachers TeacherToReturn = this._TeacherRepository.GetOne(id);
            if (TeacherToReturn != null)
            {
                return TeacherToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found in the Teachers' database.");
            }
        }




        // THREE NON-CRUD METHODS
        public IEnumerable<KeyValuePair<string, int>> TeacherEarnings()
        {
            var TotalEarning = from artists in this._TeacherRepository.GetAll().ToList()
                               join reservations in this._ReservationsRepository.GetAll().ToList()
                               on artists.Id equals reservations.TeacherId
                               group reservations by reservations.TeacherId.Value into gr
                               select new KeyValuePair<string, int>
                               (_TeacherRepository.GetOne(gr.Key).Name, (gr.Count()) * _TeacherRepository.GetOne(gr.Key).Price);
            return TotalEarning;
        }
        public List<KeyValuePair<string, int>> MostPaidTeacher()
        {
            int max = TeacherEarnings().Max(t => t.Value);
            string[] maxNums = TeacherEarnings().Where(x => x.Value == max).Select(x => x.Key).ToArray();
            List<KeyValuePair<string, int>> r = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < maxNums.Length; i++)
            {
                r.Add(new KeyValuePair<string, int>(maxNums[i], max));
            }
            return r;

        }
        public List<KeyValuePair<string, int>> LessPaidTeacher()
        {
            int min = TeacherEarnings().Min(t => t.Value);
            string[] minNums = TeacherEarnings().Where(x => x.Value == min).Select(x => x.Key).ToArray();
            List<KeyValuePair<string, int>> r = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < minNums.Length; i++)
            {
                r.Add(new KeyValuePair<string, int>(minNums[i], min));
            }
            return r;
        }
    }
}
