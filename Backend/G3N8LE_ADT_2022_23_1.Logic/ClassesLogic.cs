using G3N8LE_ADT_2022_23_1.Models;
using G3N8LE_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Logic
{
    public class ClassesLogic : IClassesLogic
    {
        protected IClassesRepository _ClassesRepository;

        public ClassesLogic(IClassesRepository classesRepository)
        {
            _ClassesRepository = classesRepository;
        }

        public void UpdateClassCost(Classes clas)
        {
            this._ClassesRepository.UpdatePrice(clas.Id, clas.Price);
        }
        public IEnumerable<Classes> GetAllClasses()
        {
            return this._ClassesRepository.GetAll();
        }
        public Classes GetClass(int id)
        {
            Classes ClassToReturn = this._ClassesRepository.GetOne(id);
            if (ClassToReturn != null)
            {
                return ClassToReturn;
            }
            else
            {
                throw new Exception("The ID is not in the Classes database.");
            }
        }
        public Classes AddNewClass(Classes clas)
        {
            if (clas.Name == null)
            {
                throw new ArgumentException("An error occured. Please enter your name.");
            }
            else
            {
                this._ClassesRepository.Add(clas);
                return clas;
            }

        }
        public void DeleteClass(int id)
        {
            Classes ClassToDelete = this._ClassesRepository.GetOne(id);
            if (ClassToDelete != null)
            {
                this._ClassesRepository.Delete(ClassToDelete);
            }
            else
            {
                throw new Exception("The ID is not in the Classes database.");
            }
        }
    }
}
