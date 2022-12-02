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
    public class TeachersLogicTester
    {
        TeachersLogic TL;
        [SetUp]
        public void Init()
        {
            var MockTeacherRepository = new Mock<ITeachersRepository>();
            var MockReservationsRepository = new Mock<IReservationsRepository>();
            var teachers = new List<Teachers>()
            {
                new Teachers(){Id=1,Name="teacher1",Branch="branch1",Price=100,Duration=1 },
                new Teachers(){Id=2,Name="teacher2",Branch="branch2",Price=200,Duration=1 },
                new Teachers(){Id=3,Name="teacher3",Branch="branch3",Price=300,Duration=1 },
                new Teachers(){Id=4,Name="teacher4",Branch="branch4",Price=400,Duration=1 },
                new Teachers(){Id=5,Name="teacher5",Branch="branch5",Price=500,Duration=1 }
            }.AsQueryable();
            var Reservations = new List<Reservations>()
            {
                new Reservations(){Id = 1 , StudentId=5,TeacherId=4,DateTime=new DateTime(2022,11,21) },
                new Reservations(){Id = 2 , StudentId=2,TeacherId=5,DateTime=new DateTime(2022,11,22) },
                new Reservations(){Id = 3 , StudentId=2,TeacherId=2,DateTime=new DateTime(2022,11,23) },
                new Reservations(){Id = 4 , StudentId=1,TeacherId=3,DateTime=new DateTime(2022,11,29) },
                new Reservations(){Id = 5 , StudentId=1,TeacherId=1,DateTime=new DateTime(2022,11,20) }
            }.AsQueryable();

            MockTeacherRepository.Setup((t) => t.GetAll()).Returns(teachers);
            MockReservationsRepository.Setup((t) => t.GetAll()).Returns(Reservations);
            for (int i = 0; i < 5; i++)
            {
                MockTeacherRepository.Setup((t) => t.GetOne(i + 1)).Returns(teachers.ToList()[i]);
            }
            TL = new TeachersLogic(MockTeacherRepository.Object, MockReservationsRepository.Object);
        }
        [Test]
        public void AddNewTeacherTest()
        {
            Teachers teach = new Teachers() { Name = "teacher6", Duration = 1, Price = 600, Branch = "brach6" };
            //Act
            Teachers artist6 = TL.AddNewTeacher(teach);
            //Arrange
            Assert.That(artist6.Name, Is.EqualTo("teacher6"));
        }
        [Test]
        public void GetTeacherTest()
        {
            //ACT
            var result = this.TL.GetTeacher(1).Name;
            var expected = "teacher1";
            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void DeleteTeacherTest_Throws()
        {
            //Arrange
            Assert.Throws<ArgumentException>(() => TL.DeleteTeacher(100));
        }
        [Test]
        public void GetTeacherTest_ThrowsArgumentException()
        {
            //Arrange
            Assert.Throws<ArgumentException>(() => TL.DeleteTeacher(100));
        }

        //Tests for Non-crud functions
        [Test]
        public void MostPaidTeacherTest()
        {
            //act
            var result = TL.MostPaidTeacher();
            var expected = new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("teacher5", 500) };
            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void LessPaidTeacherTest()
        {
            //act
            var result = TL.LessPaidTeacher();
            var expected = new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("teacher1", 100) };
            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
