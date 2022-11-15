using G3N8LE_ADT_2022_23_1.Models;
using G3N8LE_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public class ReservationsLogic : IReservationsLogic
    {
        protected IReservationsRepository _ReservationsRepository;
        protected IStudentsRepository _studentsrepo;
        protected ITeachersRepository _teachersrepo;

        public ReservationsLogic(IReservationsRepository reservationsRepository, IStudentsRepository studentsrepo, ITeachersRepository teachersrepo)
        {
            _ReservationsRepository = reservationsRepository;
            _studentsrepo = studentsrepo;
            _teachersrepo = teachersrepo;
        }

        public void UpdateReservationDate(Reservations reser)
        {
            this._ReservationsRepository.UpdateDate(reser.Id, reser.DateTime);
        }
        public Reservations AddNewReservation(Reservations reser)
        {
            if (_studentsrepo.GetOne((int)reser.StudentId) == null || _teachersrepo.GetOne((int)reser.TeacherId) == null)
            {
                throw new Exception("Data is invalid.");
            }
            else
            {
                this._ReservationsRepository.Add(reser);
                return reser;
            }

        }
        public void DeleteReservation(int id)
        {
            Reservations ReservationToDelete = this._ReservationsRepository.GetOne(id);
            if (ReservationToDelete != null)
            {
                this._ReservationsRepository.Delete(ReservationToDelete);
            }
        }
        public IEnumerable<Reservations> GetAllReservations()
        {
            return this._ReservationsRepository.GetAll();
        }
        public Reservations GetReservation(int id)
        {
            Reservations ReservationToReturn = this._ReservationsRepository.GetOne(id);
            if (ReservationToReturn != null)
            {
                return ReservationToReturn;
            }
            else
            {
                throw new Exception("The ID could not found in the Reservations database.");
            }
        }
    }
}
