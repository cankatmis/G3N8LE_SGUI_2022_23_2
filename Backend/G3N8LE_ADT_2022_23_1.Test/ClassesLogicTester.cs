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
    public class ClassesLogicTester
    {
        ClassesLogic CL;
        [SetUp]
        public void Init()
        {
            var MockClassesRepository = new Mock<IClassesRepository>();
            var classes = new List<Classes>()
            {
                new Classes(){Id=1,Name="class1",Price=1,Grading=2},
                new Classes(){Id=2,Name="class2",Price=2,Grading=3},
                new Classes(){Id=3,Name="class3",Price=3,Grading=4},
                new Classes(){Id=4,Name="class4",Price=4,Grading=5},
                new Classes(){Id=5,Name="class5",Price=5,Grading=6},
            }.AsQueryable();
            MockClassesRepository.Setup((t) => t.GetAll()).Returns(classes);
            for (int i = 0; i < 5; i++)
            {
                MockClassesRepository.Setup((t) => t.GetOne(i + 1)).Returns(classes.ToList()[i]);
            }
            CL = new ClassesLogic(MockClassesRepository.Object);
        }

        [Test]
        public void AddClassTest_ThrowsArgumentException()
        {
            Classes clas = new Classes() { Name = null, Price = 100, Grading = 2 };
            //Arrange
            Assert.Throws<ArgumentException>(() => CL.AddNewClass(clas));
        }
        [Test]
        public void DeleteClassTest_ThrowsException()
        {
            //Arrange
            Assert.Throws<Exception>(() => CL.DeleteClass(100));
        }
    }
}
