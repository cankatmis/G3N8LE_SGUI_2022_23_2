using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G3N8LE_ADT_2022_23_1.Models;

namespace G3N8LE_ADT_2022_23_1.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
        {
            this.Database.EnsureCreated();
        }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<ReservationsServices> ConnectorTable { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Classes class1 = new Classes() { Id = 1, Name = "English ", Price = 500, Grading = 5 };
            Classes class2 = new Classes() { Id = 2, Name = "Hungarian ", Price = 600, Grading = 5 };
            Classes class3 = new Classes() { Id = 3, Name = "Mathematics ", Price = 1000, Grading = 4 };
            Classes class4 = new Classes() { Id = 4, Name = "Turkish ", Price = 600, Grading = 5 };
            Classes class5 = new Classes() { Id = 5, Name = "History ", Price = 700, Grading = 4 };
            Classes class6 = new Classes() { Id = 6, Name = "Geography ", Price = 700, Grading = 4 };

            Teachers teacher1 = new Teachers() { Id = 1, Name = "Can", Branch = "Turkish", Duration = 1, Price = 600 };
            Teachers teacher2 = new Teachers() { Id = 2, Name = "Alice", Branch = "Mathematics", Duration = 2, Price = 2000 };
            Teachers teacher3 = new Teachers() { Id = 3, Name = "Bob", Branch = "History", Duration = 1, Price = 700 };
            Teachers teacher4 = new Teachers() { Id = 4, Name = "Chris", Branch = "Hungarian", Duration = 3, Price = 1800 };
            Teachers teacher5 = new Teachers() { Id = 5, Name = "Duna", Branch = "English", Duration = 1, Price = 500 };
            Teachers teacher6 = new Teachers() { Id = 6, Name = "Foo", Branch = "Geography", Duration = 1, Price = 700 };

            Students student1 = new Students() { Id = 1, Name = "Ali ", PhoneNumber = 0610203040, City = "Bolu", Email = "ali@gmail.com" };
            Students student2 = new Students() { Id = 2, Name = "Mustafa", PhoneNumber = 0650607080, City = "Bolu", Email = "mustafa@gmail.com" };
            Students student3 = new Students() { Id = 3, Name = "David", PhoneNumber = 0690102030, City = "Ankara", Email = "david@gmail.com" };
            Students student4 = new Students() { Id = 4, Name = "Freya", PhoneNumber = 0640506070, City = "Bolu", Email = "freya@gmail.com" };
            Students student5 = new Students() { Id = 5, Name = "Izabella", PhoneNumber = 0680901020, City = "Eskisehir", Email = "izabella@gmail.com" };
            Students student6 = new Students() { Id = 6, Name = "Utku", PhoneNumber = 0630405060, City = "Ankara", Email = "utku@gmail.com" };
            Students student7 = new Students() { Id = 7, Name = "Jamila", PhoneNumber = 0670809010, City = "Ankara", Email = "jamila@gmail.com" };
            Students student8 = new Students() { Id = 8, Name = "Berrak", PhoneNumber = 0620304050, City = "Eskisehir", Email = "berrak@gmail.com" };
            Students student9 = new Students() { Id = 9, Name = "Meryem", PhoneNumber = 0660708090, City = "Eskisehir", Email = "meryem@gmail.com" };
            Students student10 = new Students() { Id = 10, Name = "Selay", PhoneNumber = 0610203040, City = "Eskisehir", Email = "selay@gmail.com" };
            Students student11 = new Students() { Id = 11, Name = "Fatih", PhoneNumber = 0650607080, City = "Bolu", Email = "fatih@gmail.com" };

            Reservations reservation1 = new Reservations() { Id = 1, StudentId = student1.Id, TeacherId = teacher1.Id, DateTime = new DateTime(2022, 09, 08) };
            Reservations reservation2 = new Reservations() { Id = 2, StudentId = student2.Id, TeacherId = teacher3.Id, DateTime = new DateTime(2022, 09, 09) };
            Reservations reservation3 = new Reservations() { Id = 3, StudentId = student5.Id, TeacherId = teacher2.Id, DateTime = new DateTime(2022, 09, 10) };
            Reservations reservation4 = new Reservations() { Id = 4, StudentId = student10.Id, TeacherId = teacher1.Id, DateTime = new DateTime(2022, 09, 11) };
            Reservations reservation5 = new Reservations() { Id = 5, StudentId = student4.Id, TeacherId = teacher6.Id, DateTime = new DateTime(2022, 09, 12) };
            Reservations reservation6 = new Reservations() { Id = 6, StudentId = student11.Id, TeacherId = teacher2.Id, DateTime = new DateTime(2022, 09, 14) };
            Reservations reservation7 = new Reservations() { Id = 7, StudentId = student6.Id, TeacherId = teacher5.Id, DateTime = new DateTime(2022, 09, 14) };
            Reservations reservation8 = new Reservations() { Id = 8, StudentId = student8.Id, TeacherId = teacher6.Id, DateTime = new DateTime(2022, 04, 15) };
            Reservations reservation9 = new Reservations() { Id = 9, StudentId = student3.Id, TeacherId = teacher4.Id, DateTime = new DateTime(2022, 08, 16) };
            Reservations reservation10 = new Reservations() { Id = 10, StudentId = student7.Id, TeacherId = teacher1.Id, DateTime = new DateTime(2022, 10, 17) };
            Reservations reservation11 = new Reservations() { Id = 11, StudentId = student9.Id, TeacherId = teacher2.Id, DateTime = new DateTime(2022, 11, 18) };

            ReservationsServices connection1 = new ReservationsServices() { Id = 1, ReservationId = reservation1.Id, ClassId = class1.Id };
            ReservationsServices connection2 = new ReservationsServices() { Id = 2, ReservationId = reservation2.Id, ClassId = class2.Id };
            ReservationsServices connection3 = new ReservationsServices() { Id = 3, ReservationId = reservation2.Id, ClassId = class3.Id };
            ReservationsServices connection4 = new ReservationsServices() { Id = 4, ReservationId = reservation2.Id, ClassId = class4.Id };
            ReservationsServices connection5 = new ReservationsServices() { Id = 5, ReservationId = reservation3.Id, ClassId = class5.Id };
            ReservationsServices connection6 = new ReservationsServices() { Id = 6, ReservationId = reservation4.Id, ClassId = class6.Id };
            ReservationsServices connection7 = new ReservationsServices() { Id = 7, ReservationId = reservation5.Id, ClassId = class1.Id };
            ReservationsServices connection8 = new ReservationsServices() { Id = 8, ReservationId = reservation5.Id, ClassId = class2.Id };
            ReservationsServices connection9 = new ReservationsServices() { Id = 9, ReservationId = reservation6.Id, ClassId = class3.Id };
            ReservationsServices connection10 = new ReservationsServices() { Id = 10, ReservationId = reservation7.Id, ClassId = class4.Id };
            ReservationsServices connection11 = new ReservationsServices() { Id = 11, ReservationId = reservation8.Id, ClassId = class5.Id };
            ReservationsServices connection12 = new ReservationsServices() { Id = 12, ReservationId = reservation9.Id, ClassId = class6.Id };
            ReservationsServices connection13 = new ReservationsServices() { Id = 13, ReservationId = reservation10.Id, ClassId = class1.Id };
            ReservationsServices connection14 = new ReservationsServices() { Id = 14, ReservationId = reservation11.Id, ClassId = class2.Id };
            ReservationsServices connection15 = new ReservationsServices() { Id = 15, ReservationId = reservation2.Id, ClassId = class3.Id };
            ReservationsServices connection16 = new ReservationsServices() { Id = 16, ReservationId = reservation4.Id, ClassId = class4.Id };
            ReservationsServices connection17 = new ReservationsServices() { Id = 17, ReservationId = reservation5.Id, ClassId = class5.Id };
            ReservationsServices connection18 = new ReservationsServices() { Id = 18, ReservationId = reservation8.Id, ClassId = class6.Id };
            ReservationsServices connection19 = new ReservationsServices() { Id = 19, ReservationId = reservation7.Id, ClassId = class5.Id };
            ReservationsServices connection20 = new ReservationsServices() { Id = 20, ReservationId = reservation10.Id, ClassId = class1.Id };



            modelBuilder.Entity<Reservations>(entity =>
            {
                entity.HasOne(reservation => reservation.Teacher)
                      .WithMany(teacher => teacher.Reservations)
                      .HasForeignKey(reservation => reservation.TeacherId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Reservations>(entity =>
            {
                entity.HasOne(reservation => reservation.Student)
                      .WithMany(student => student.Reservations)
                      .HasForeignKey(reservation => reservation.StudentId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ReservationsServices>(entity =>
            {
                entity.HasOne(connection => connection.Reservations)
                      .WithMany(reservation => reservation.ConnectorReservationsServices)
                      .HasForeignKey(connection => connection.ReservationId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ReservationsServices>(entity =>
            {
                entity.HasOne(connection => connection.Classes)
                      .WithMany(service => service.ConnectorReservationsServices)
                      .HasForeignKey(connection => connection.ClassId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Students>().HasData(student1, student2, student3, student4, student5, student6, student7, student8, student9, student10, student11);
            modelBuilder.Entity<Teachers>().HasData(teacher1, teacher2, teacher3, teacher4, teacher5, teacher6);
            modelBuilder.Entity<Classes>().HasData(class1, class2, class3, class4, class5, class6);
            modelBuilder.Entity<Reservations>().HasData(reservation1, reservation2, reservation3, reservation4, reservation5, reservation6, reservation7, reservation8, reservation9, reservation10, reservation11);
            modelBuilder.Entity<ReservationsServices>().HasData(connection1, connection2, connection3, connection4, connection5, connection6, connection7, connection8, connection9, connection10, connection11, connection12, connection13, connection14, connection15, connection16, connection17, connection18, connection19, connection20);
        }
    }
}
