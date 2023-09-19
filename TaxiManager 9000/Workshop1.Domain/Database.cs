using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop1.Domain.Enums;
using Workshop1.Domain.Models;


namespace Workshop1.Domain
{
    public static class Database
    {
        static Database()
        {
            
            SeedData();
        }
        public static List<User> Users { get; set; } = new List<User>();
        public static List<Driver> Drivers { get; set; } = new List<Driver>();
        public static List<Car> Cars { get; set; } = new List<Car>();

        private static void SeedData()
        {
            
            Users = new List<User>()
            {
                new User ("Dimitar", "Dimitar123",Role.Administrator),
                new User ("Vasko", "Vasko123",Role.Manager),
                new User ("Spase", "Spase123",Role.Maintanance)

            };
            Cars = new List<Car>()
            {
                new Car ("Honda", "SK1021AC", new DateOnly(2022,02,02)),
                new Car ("Skoda", "SR8128AD", new DateOnly(2024,02,06)),
                new Car ("Toyota", "SR1234AE", new DateOnly(2023,05,07))
            };

            Drivers = new List<Driver>()
            {
                new Driver ("Boshko","Boshkoski", Shift.Morning,"M023123", new DateOnly (2021,03,03),Cars[0]),
                new Driver ("Nikola","Nikoloski", Shift.Afternoon,"M432353", new DateOnly (2024,04,21),Cars[1]),
                new Driver ("Daniel","Daniloski", Shift.Evening,"M86595", new DateOnly (2023,07,21),Cars[1]),
                new Driver ("Marko","Markovski", Shift.Unassigned,"M82746", new DateOnly (2024,07,21)),
                new Driver ("Darko","Darkoski", Shift.Unassigned,"M73215", new DateOnly (2024,08,21))
            };
            Cars[0].AsignedDrivers = new List<Driver>() { Drivers[0]};
            Cars[1].AsignedDrivers = new List<Driver>() { Drivers[1], Drivers[2]};
            

        }
        public static void PrintEntities<T>(this List<T> list) where T : BaseEntity
        {
            foreach (T entity in list)
            {
                entity.GetInfo();
            }
        }   

    }
}
