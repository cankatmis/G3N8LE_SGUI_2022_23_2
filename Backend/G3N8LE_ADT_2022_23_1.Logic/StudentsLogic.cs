using G3N8LE_ADT_2022_23_1.Models;
using G3N8LE_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public class StudentsLogic : IStudentsLogic
    {
        protected IReservationsRepository _ReservationsRepository;
        protected IStudentsRepository _StudentsRepository;
        public StudentsLogic(IReservationsRepository reservationsRepo, IStudentsRepository studentsRepo)
        {
            _ReservationsRepository = reservationsRepo;
            _StudentsRepository = studentsRepo;
        }

        public void UpdateCity(Students student)
        {
            this._StudentsRepository.UpdateCity(student.Id, student.City);
        }
        public Students AddNewStudent(Students student)
        {
            if (student.Name == null)
            {
                throw new ArgumentException("An error occured. Please provide the name.");
            }
            else
            {

                this._StudentsRepository.Add(student);
                return student;
            }

        }
        public void DeleteStudent(int id)
        {
            Students StudentToDelete = this._StudentsRepository.GetOne(id);
            if (StudentToDelete != null)
            {
                this._StudentsRepository.Delete(StudentToDelete);
            }
            else
            {
                throw new ArgumentException("An error occured. There isn't any student with the ID provided.");
            }
        }
        public IEnumerable<Students> GetAllStudents()
        {
            return this._StudentsRepository.GetAll();
        }
        public Students GetStudent(int id)
        {
            Students studentToReturn = this._StudentsRepository.GetOne(id);
            if (studentToReturn != null)
            {
                return studentToReturn;
            }
            else
            {
                throw new Exception("The ID can't be found in the Students database.");
            }
        }


        // TWO NON-CRUD METHODS
        public List<KeyValuePair<int, int>> BestStudent()
        {


            var BestStudent = from student in this._StudentsRepository.GetAll().ToList()
                              join Reservations in this._ReservationsRepository.GetAll().ToList()
                              on student.Id equals Reservations.StudentId
                              group Reservations by Reservations.StudentId.Value into gr
                              select new
                              {
                                  id = gr.Key,
                                  c = gr.Count()
                              };
            int max = BestStudent.AsEnumerable().Max(t => t.c);

            int[] maxNumOfReservations = BestStudent.Where(x => x.c == max).Select(x => x.id).ToArray();
            List<KeyValuePair<int, int>> r = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < maxNumOfReservations.Length; i++)
            {
                r.Add(new KeyValuePair<int, int>(maxNumOfReservations[i], max));
            }
            return r;
        }
        public List<KeyValuePair<int, int>> WorstStudent()
        {
            var WorstStudent = from student in this._StudentsRepository.GetAll().ToList()
                               join Reservations in this._ReservationsRepository.GetAll().ToList()
                               on student.Id equals Reservations.StudentId
                               group Reservations by Reservations.StudentId.Value into grp
                               select new
                               {
                                   id = grp.Key,
                                   c = grp.Count()
                               };
            int min = WorstStudent.AsEnumerable().Min(t => t.c);
            int[] maxNumOfReservations = WorstStudent.Where(x => x.c == min).Select(x => x.id).ToArray();
            List<KeyValuePair<int, int>> r = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < maxNumOfReservations.Length; i++)
            {
                r.Add(new KeyValuePair<int, int>(maxNumOfReservations[i], min));
            }

            return r;
        }
        public int ReservationsNumber(int id)
        {
            if (GetStudent(id) == null)
            {
                throw new Exception("The student ID couldn't found in the database.");
            }
            else
            {
                var res = this._ReservationsRepository.GetAll().Count(x => x.StudentId == id);
                return res;
            }


        }



    }
}
