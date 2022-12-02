using ConsoleTools;
using G3N8LE_ADT_2022_23_1.Models;
using System;
using System.Collections.Generic;

namespace G3N8LE_ADT_2022_23_1.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);
            RestService rest = new RestService("http://localhost:60617");

            var MenuForStudentsadmin = new ConsoleMenu()
                .Add(">> READ By Id", () => ReadStudentById(rest))
                .Add(">> READ All", () => ReadAllStudents(rest))
                .Add(">> DELETE", () => DeleteStudent(rest))
                .Add(">> Best Student (non-crud)", () => BestStudent(rest))
                .Add(">> Worst Student (non-crud)", () => WorstStudent(rest))
                .Add(">> Reservations count (non-crud) ", () => CountResers(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForStudents = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewStudent(rest))
                .Add(">> Add Reservation", () => AddNewReservation(rest))
                .Add(">> Read all Classes", () => ReadAllClasses(rest))
                .Add(">> Read all Teachers", () => ReadAllTeachers(rest))
                .Add(">> UpdateCity", () => UpdateStudentCity(rest))
                .Add(">> DELETE", () => DeleteStudent(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForTeachers = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewTeacher(rest))
                .Add(">> READ By Id", () => ReadTeacherById(rest))
                .Add(">> READ All", () => ReadAllTeachers(rest))
                .Add(">> UpdateCost", () => UpdateTeachercost(rest))
                .Add(">> DELETE", () => DeleteTeacher(rest))
                .Add(">> Teachers Earnings (non-crud)", () => Teacherearnings(rest))
                .Add(">> Most Paid Teacher (non-crud)", () => MostPaidTeacher(rest))
                .Add(">> Less Paid Teacher (non-crud)", () => LessPaidTeacher(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForReservations = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewReservation(rest))
                .Add(">> READ By Id", () => ReadReservationById(rest))
                .Add(">> READ All", () => ReadAllReservations(rest))
                .Add(">> UpdateDate", () => UpdateReservationdate(rest))
                .Add(">> DELETE", () => DeleteReservation(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForClasses = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewClass(rest))
                .Add(">> READ By Id", () => ReadClassById(rest))
                .Add(">> READ All", () => ReadAllClasses(rest))
                .Add(">> UpdateCost", () => UpdateClasscost(rest))
                .Add(">> DELETE", () => DeleteClass(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForReservationsServices = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewConnection(rest))
                .Add(">> READ By Id", () => ReadConnectionById(rest))
                .Add(">> READ All", () => ReadAllConnections(rest))
                .Add(">> DELETE", () => DeleteConnection(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });

            var menuForAdministrator = new ConsoleMenu(args, level: 0)
                .Add(">> Students", () => MenuForStudentsadmin.Show())
                .Add(">> Teachers ", () => MenuForTeachers.Show())
                .Add(">> Reservations ", () => MenuForReservations.Show())
                .Add(">> Classes ", () => MenuForClasses.Show())
                .Add(">> ReservationsServicesConnections ", () => MenuForReservationsServices.Show())
                .Add(">> Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Blue;
                });
            var MainMenu = new ConsoleMenu(args, level: 0)
                .Add(">> Student", () => MenuForStudents.Show())
                .Add(">> Administrator ", () => menuForAdministrator.Show())
                .Add(">> Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Blue;
                });

            MainMenu.Show();

        }
        #region studentsMethods
        private static void AddNewStudent(RestService rest)
        {
            try
            {
                Console.WriteLine("\n:: New Student ::\n");
                Console.Write("Student's Name : ");
                string name = Console.ReadLine();

                Console.Write("Student's City : ");
                string city = Console.ReadLine();

                Console.Write("Student's Email : ");
                string email = Console.ReadLine();

                Console.Write("Student's Phone number : ");
                int phoneNumber = int.Parse(Console.ReadLine());

                Students student = new Students() { City = city, Email = email, Name = name, PhoneNumber = phoneNumber };

                rest.Post<Students>(student, "Students");

                Console.WriteLine("\n A student with name " + name.ToString().ToUpper() + " has been added to the Database\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ReadStudentById(RestService rest)
        {
            Console.Write("\n ID of Student :  ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Name",-20} {"Email",-28} {"PhoneNumber",10}  {"City",5}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Students>(id, "students").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void CountResers(RestService rest)
        {
            Console.Write("Student's ID : ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                int coun = rest.Get<int>(id, "Noncrudstudent/ReservationNUM");
                Console.WriteLine("This student has : " + coun + " reservations.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ReadAllStudents(RestService rest)
        {
            Console.WriteLine("\n   ALL Students :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Name",-20} {"Email",-28} {"PhoneNumber",10} {"City",5}");
            Console.ResetColor();
            var students = rest.Get<Students>("students");
            students.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateStudentCity(RestService rest)
        {
            Console.WriteLine("\n  Student's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());

                Console.Write("\n New City : ");
                string city = Console.ReadLine();
                Students f1 = rest.Get<Students>(id, "students");
                f1.City = city;

                rest.Put<Students>(f1, "students");


                Console.WriteLine("City Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteStudent(RestService rest)
        {
            Console.WriteLine("\n Student's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Student who will be deleted  has ID : " + id);
                rest.Delete(id, "students");
                Console.WriteLine("  Student deleted! ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void BestStudent(RestService rest)
        {
            var beststudents = rest.Get<KeyValuePair<int, int>>("Noncrudstudent/BestStudents");
            foreach (var item in beststudents)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Best Student Id : " + item.Key + ", Reservations number : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void WorstStudent(RestService rest)
        {
            var worststudents = rest.Get<KeyValuePair<int, int>>("Noncrudstudent/WorstStudents");
            foreach (var item in worststudents)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Worst Student Id : " + item.Key + ", Reservations number : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion

        #region TeachersMethods
        private static void AddNewTeacher(RestService rest)
        {
            Console.WriteLine("\n:: New Teacher ::\n");
            Console.Write("Teacher's Name : ");
            string name = Console.ReadLine();

            Console.Write("Teacher's Duration (hours) : ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Teacher's price : ");
            int price = int.Parse(Console.ReadLine());

            Console.Write("Teacher's branch  : ");
            string branch = Console.ReadLine();

            rest.Post<Teachers>(new Teachers() { Name = name, Duration = duration, Price = price, Branch = branch }, "teachers");

            Console.WriteLine("\n An teacher with the name  " + name.ToString().ToUpper() + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadTeacherById(RestService rest)
        {
            Console.WriteLine("\n ID of Teacher :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} |  {"Duration"}  {"Price",10}  {"Branch",10} {"Name",15}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Teachers>(id, "teachers").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllTeachers(RestService rest)
        {
            Console.WriteLine("\n   ALL Teachers :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} |  {"Duration"}  {"Price",10}  {"Category",10} {"Name",15}");
            Console.ResetColor();
            var teachers = rest.Get<Teachers>("teachers");
            teachers.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateTeachercost(RestService rest)
        {
            Console.WriteLine("\n  Teacher's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());

                Console.Write("\n New Cost : ");
                int cost = int.Parse(Console.ReadLine());

                Teachers teacher = rest.Get<Teachers>(id, "teachers");
                teacher.Price = cost;

                rest.Put<Teachers>(teacher, "teachers");


                Console.WriteLine("Cost Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteTeacher(RestService rest)
        {
            Console.WriteLine("\n Teacher's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Teacher to be deleted has ID :  " + id);
                rest.Delete(id, "teachers");
                Console.WriteLine("  Teacher deleted! ");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void Teacherearnings(RestService rest)
        {
            var teacherearnings = rest.Get<KeyValuePair<string, int>>("Noncrudteacher/TeachersEarnings");
            foreach (var item in teacherearnings)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("TEACHER NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void MostPaidTeacher(RestService rest)
        {
            var Mostpaidteacher = rest.Get<KeyValuePair<string, int>>("Noncrudteacher/MostPaidTeachers");
            foreach (var item in Mostpaidteacher)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("TEACHER NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void LessPaidTeacher(RestService rest)
        {
            var Lesspaidteacher = rest.Get<KeyValuePair<string, int>>("Noncrudteacher/LessPaidTeachers");
            foreach (var item in Lesspaidteacher)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("TEACHER NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion

        #region ReservationsMethods
        private static void AddNewReservation(RestService rest)
        {
            Console.WriteLine("\n:: New Reservation ::\n");


            Console.Write("Student ID  : ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Teacher ID : : ");
            int teacherId = int.Parse(Console.ReadLine());

            Console.Write(" Date [yyyy-MM-dd HH:mm] : ");
            DateTime dateTime = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);
            try
            {
                rest.Post<Reservations>(new Reservations() { StudentId = studentId, TeacherId = teacherId, DateTime = dateTime }, "reservations");
                Console.WriteLine("\n A Reservation for a student with ID " + studentId + " has been added to the Database\n");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadReservationById(RestService rest)
        {
            Console.WriteLine("\n ID of Reservation :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Student Id ",-20} {"DateTime",10} {"Teacher Id",25}");
                Console.ResetColor();
                var re = rest.Get<Reservations>(id, "reservations");
                Console.WriteLine(re.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllReservations(RestService rest)
        {
            Console.WriteLine("\n   ALL Reservations :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Student Id ",-20} {"DateTime",10} {"Teacher Id",25}");
            Console.ResetColor();
            var reservations = rest.Get<Reservations>("reservations");
            reservations.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateReservationdate(RestService rest)
        {
            Console.WriteLine("\n  Reservation's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());

                Console.Write("\n New Date [yyyy - MM - dd HH: mm] :  ");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);
                Reservations r1 = rest.Get<Reservations>(id, "reservations");
                r1.DateTime = date;

                rest.Put<Reservations>(r1, "reservations");


                Console.WriteLine("Date Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteReservation(RestService rest)
        {
            Console.WriteLine("Reservation's ID which will be deleted ");

            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, "reservations");
            Console.WriteLine("  Reservation deleted! ");

            Console.ReadLine();
        }
        #endregion

        #region ClassesMethods
        private static void AddNewClass(RestService rest)
        {
            Console.WriteLine("\n:: New Class ::\n");

            Console.Write("Class' name :  : ");
            string name = Console.ReadLine();

            Console.Write("Class' price  : ");
            int price = int.Parse(Console.ReadLine());

            Console.Write("Class' rating ( {1..10} )  : ");
            int grading = int.Parse(Console.ReadLine());

            rest.Post<Classes>(new Classes() { Name = name, Price = price, Grading = grading }, "Classes");


            Console.WriteLine("\n A Class with name  " + name.ToString().ToUpper() + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadClassById(RestService rest)
        {
            Console.WriteLine("\n ID of Class :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Rating",2}/10 {"Price",7}  {"Name",10}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Classes>(id, "classes").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllClasses(RestService rest)
        {
            Console.WriteLine("\n   ALL Classes :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Rating",2}/10 {"Price",7}  {"Name",10}");
            Console.ResetColor();
            var classes = rest.Get<Classes>("classes");
            classes.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateClasscost(RestService rest)
        {
            Console.WriteLine("\n  Class' ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());

                Console.Write("\n New Cost :  ");
                int cost = int.Parse(Console.ReadLine());
                Classes s1 = rest.Get<Classes>(id, "Classes");
                s1.Price = cost;

                rest.Put<Classes>(s1, "Classes");

                Console.WriteLine("Cost Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteClass(RestService rest)
        {
            Console.WriteLine("Class' ID which will be deleted : ");
            int id = int.Parse(Console.ReadLine());

            rest.Delete(id, "classes");
            Console.WriteLine("  Reservation deleted! ");

            Console.ReadLine();
        }
        #endregion

        #region ConnectionsMethods
        private static void AddNewConnection(RestService rest)
        {
            Console.WriteLine("\n:: New Connection ::\n");

            Console.Write("Reservation's ID  : ");
            int reservationId = int.Parse(Console.ReadLine());

            Console.Write("Class' ID: : ");
            int classid = int.Parse(Console.ReadLine());

            rest.Post<ReservationsServices>(new ReservationsServices() { ReservationId = reservationId, ClassId = classid }, "Reservationsservices");

            Console.WriteLine("\n A Connection has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadConnectionById(RestService rest)
        {
            Console.WriteLine("\n ID of Connection :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{"Id",3} | {"ReservationId",5} {"ClassId",10}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<ReservationsServices>(id, "reservationsservices").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllConnections(RestService rest)
        {
            Console.WriteLine("\n   ALL Connections :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{"Id",3} | {"ReservationId",5}\t {"ClassId",10}");
            Console.ResetColor();
            var reservationssers = rest.Get<ReservationsServices>("reservationsservices");
            reservationssers.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void DeleteConnection(RestService rest)
        {
            Console.WriteLine("Connection's ID which will be deleted ");
            int id = int.Parse(Console.ReadLine());

            rest.Delete(id, "reservationsservices");
            Console.WriteLine("  Connection deleted! ");

            Console.ReadLine();
        }
        #endregion



    }
}
