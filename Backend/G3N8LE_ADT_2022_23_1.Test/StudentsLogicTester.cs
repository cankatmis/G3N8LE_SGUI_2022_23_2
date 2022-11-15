using G3N8LE_ADT_2022_23_1.Logic;
using G3N8LE_ADT_2022_23_1.Models;
using G3N8LE_ADT_2022_23_1.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Test
{
    [TestFixture]
    public class StudentsLogicTester
    {
        StudentsLogic SL;
        [SetUp]
        public void Init()
        {
            var MockStudentRepository = new Mock<IStudentsRepository>();
            var MockReservationsRepository = new Mock<IReservationsRepository>();
            var students = new List<Students>()
            {
                new Students(){Id =1,City="Budapest1",Email="student1@gmail.com",Name="student1",PhoneNumber=11111111},
                new Students(){Id =2,City="Budapest2",Email="student2@gmail.com",Name="student2",PhoneNumber=22222222},
                new Students(){Id =3,City="Budapest3",Email="student3@gmail.com",Name="student3",PhoneNumber=33333333},
                new Students(){Id =4,City="Budapest4",Email="student4@gmail.com",Name="student4",PhoneNumber=44444444},
                new Students(){Id =5,City="Budapest5",Email="student5@gmail.com",Name="student5",PhoneNumber=55555555}
            }.AsQueryable();
            var Reservations = new List<Reservations>()
            {
                new Reservations(){Id = 1 , StudentId=5,TeacherId=4,DateTime=new DateTime(2021,11,21) },
                new Reservations(){Id = 2 , StudentId=2,TeacherId=5,DateTime=new DateTime(2021,11,22) },
                new Reservations(){Id = 3 , StudentId=2,TeacherId=2,DateTime=new DateTime(2021,11,23) },
                new Reservations(){Id = 4 , StudentId=1,TeacherId=3,DateTime=new DateTime(2021,11,29) },
                new Reservations(){Id = 5 , StudentId=1,TeacherId=1,DateTime=new DateTime(2021,11,20) }
            }.AsQueryable();
            MockStudentRepository.Setup((t) => t.GetAll()).Returns(students);
            MockReservationsRepository.Setup((t) => t.GetAll()).Returns(Reservations);
            for (int i = 0; i < 5; i++)
            {
                MockStudentRepository.Setup((t) => t.GetOne(i + 1)).Returns(students.ToList()[i]);
            }
            SL = new StudentsLogic(MockReservationsRepository.Object, MockStudentRepository.Object);
        }

        // CRUD TESTS
        [Test]
        public void AddNewStudentTest_ThrowsArgumentException()
        {
            Students student = new Students() { City = "budapest6", Email = "student6@gmail.com", Name = null, PhoneNumber = 66666666 };
            //Arrange
            Assert.Throws<ArgumentException>(() => SL.AddNewStudent(student));
        }
        [Test]
        public void AddNewStudentTest()
        {
            Students student = new Students() { City = "budapest6", Email = "student6@gmail.com", Name = "student6", PhoneNumber = 66666666 };
            //Act

            Students student6 = SL.AddNewStudent(student);
            //Arrange
            Assert.That(student6.Name, Is.EqualTo("student6"));
        }

        [Test]
        public void DeleteStudentTest_ThrowsArgumentException()
        {
            //Arrange
            Assert.Throws<ArgumentException>(() => SL.DeleteStudent(100));
        }

        // NON-CRUD TESTS
        [Test]
        public void BestStudent()
        {
            //act 
            var result = SL.BestStudent();
            var expected = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(1, 2), new KeyValuePair<int, int>(2, 2) };

            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void WorstStudent()
        {
            //act 
            var result = SL.WorstStudent();
            var expected = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(5, 1) };

            Assert.That(result, Is.EqualTo(expected));

        }

    }
}
