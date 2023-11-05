using Project.DAL.Context;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Init
{
    public class MyInit : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            #region Employee
            Employee e1 = new Employee
            {
                UserName = "Admin",
                PassWord = DanteCrypto.Sifrele("1234"),
                FirstName = "Ayfer",
                LastName = "Gülcü",
                Department = "Ceo",
                Role = UserRole.Admin,
                ID = 1,

            
            };
            Employee e2 = new Employee
            {
                UserName = "SemiAdmin",
                PassWord = DanteCrypto.Sifrele("1234"),
                FirstName = "Kasım",
                LastName = "Balcı",
                Department = "Konsul",
                Role = UserRole.Director,
                ID = 2
                

            };
            Employee e3 = new Employee
            {
                UserName = "Automation",
                PassWord = DanteCrypto.Sifrele("1234"),
                FirstName = "Erhan",
                LastName = "Gül",
                Department = "Gise",
                Role = UserRole.Automation,
                ID = 3

            };
            context.Employees.Add(e1);
            context.Employees.Add(e2);
            context.Employees.Add(e3);
            context.SaveChanges();
            #endregion
            #region Customer
            Customer c = new Customer
            {
                UserName = "Brk",
                PassWord = DanteCrypto.Sifrele("1234"),
                FirstName = "Burak",
                LastName = "Balcı",
                Role = UserRole.Customer,
                Active = true,
                CustomerCategory = CustomerRole.Full,
                Job = "Developer",
                Age = "24",
                Email = "Seksenler2011@hotmail.com",
                PhoneNumber = "05377958822",
                EmployeeID = 3,
                Gender = "Male",
                MonthlyDate = DateTime.Now

            };
            context.Customers.Add(c);
            context.SaveChanges();
            #endregion 
            #region Salon
            Salon salon1 = new Salon()
            {
                SalonNo = 1,
                Capacity = 14,

            };
            Salon salon2 = new Salon()
            {
                SalonNo = 2,
                Capacity = 14
            };
            Salon salon3 = new Salon()
            {
                SalonNo = 3,
                Capacity = 14
            };
            Salon salon4 = new Salon()
            {
                SalonNo = 4,
                Capacity = 14
            };
            
            context.Salons.Add(salon1);
            context.Salons.Add(salon2);
            context.Salons.Add(salon3);
            context.Salons.Add(salon4);
            context.SaveChanges();
            #endregion
            #region Movie
            Movie m1 = new Movie()
            {
                FilmTitle = "Kötü Mal",
                ReleaseDate = new DateTime(2023, 10, 8),
                TakedownDate = new DateTime(2023, 12, 8),
                EmployeeID = 666,
                SalonID = 1,
                ImagePath = "a16-2-3.jpeg",
                
            };
           
            Movie m2 = new Movie()
            {
                FilmTitle = "Faqbadi",
                ReleaseDate = new DateTime(2023, 10, 8),
                TakedownDate = new DateTime(2023, 12, 8),
                EmployeeID = 666,
                SalonID = 2,
                ImagePath = "a14-6-2.jpeg"
            };
            Movie m3 = new Movie()
            {
                FilmTitle = "Kuru Hasan",
                ReleaseDate = new DateTime(2023, 10, 8),
                TakedownDate = new DateTime(2023, 12, 8),
                EmployeeID = 666,
                SalonID = 3,
                ImagePath = "a18-2-4.jpeg"
            };
            Movie m4 = new Movie()
            {
                FilmTitle = "Doyamadım",
                ReleaseDate = new DateTime(2023, 10, 8),
                TakedownDate = new DateTime(2023, 12, 8),
                EmployeeID = 666,
                SalonID = 4,
                ImagePath = "a20-4-1.jpeg"
            };
            Movie m5 = new Movie()
            {
                FilmTitle = "Frozen",
                ReleaseDate = new DateTime(2024,4,23),
                TakedownDate = new DateTime(2024,4,23),
                EmployeeID = 666,
                SalonID = 1,
                ImagePath = "frozen-junior-novel.jpeg"
            };
            context.Movies.Add(m1);
            context.Movies.Add(m2);
            context.Movies.Add(m3);
            context.Movies.Add(m4);
            context.Movies.Add(m5);
            context.SaveChanges();
            #endregion
            #region Seans
            for (int i = 1; i < 5; i++)
            {
                Session s1 = new Session
                {
                    SessionTime = "11:00",
                    MovieID = i,
                    SalonID = i,
                };
                context.Seanses.Add(s1);
            }
            for (int i = 1; i < 5; i++)
            {
                Session s1 = new Session
                {
                    SessionTime = "14:00",
                    MovieID = i,
                    SalonID = i,
                };
                context.Seanses.Add(s1);
            }
            for (int i = 1; i < 5; i++)
            {
                Session s1 = new Session
                {
                    SessionTime = "17:00",
                    MovieID = i,
                    SalonID = i,
                };
                context.Seanses.Add(s1);
            }
            for (int i = 1; i < 5; i++)
            {
                Session s1 = new Session
                {
                    SessionTime = "20:00",
                    MovieID = i,
                    SalonID = i,
                    
                };
                context.Seanses.Add(s1);
            }
            Session s17 = new Session
            {
                SessionTime = "11:00",
                MovieID = 5,
                SalonID = 1,

            };
            Session s18 = new Session
            {
                SessionTime = "14:00",
                MovieID = 5,
                SalonID = 1,


            };
            Session s19 = new Session
            {
                SessionTime = "17:00",
                MovieID = 5,
                SalonID = 1,

            };
            Session s20 = new Session
            {
                SessionTime = "20:00",
                MovieID = 5,
                SalonID = 1,

            };
            context.Seanses.Add(s17);
            context.Seanses.Add(s18);
            context.Seanses.Add(s19);
            context.Seanses.Add(s20);
            context.SaveChanges();
            #endregion
            #region Rezervasyon
            Reservation r = new Reservation
            {
                Active = false,
                SessionID = 1,
                ResSeatNo = "lblC2",
                CustomerID = 1,
                Type = TicketType.Rez,
                TicketTime = new DateTime(2023,10,6)
            };
            context.Reservations.Add(r);
            context.SaveChanges();
            #endregion

        }
    }
}
