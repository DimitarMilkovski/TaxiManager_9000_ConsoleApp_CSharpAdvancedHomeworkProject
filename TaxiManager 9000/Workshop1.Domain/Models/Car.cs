using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop1.Helpers;

namespace Workshop1.Domain.Models
{
    public class Car : BaseEntity
    {
        private static int _carCounter = 1;
        public Car()
        {
            Id = _carCounter;
            _carCounter++;

        }
        public Car(string model, string licencePlate, DateOnly licencePlateExpiryDate)
        {
            Id = _carCounter;
            Model = model;
            LicencePlate = licencePlate;
            LicencePlateExpiryDate = licencePlateExpiryDate;
            _carCounter++;


        }

        public string Model { get; set; }
        public string LicencePlate { get; set; }
        public DateOnly LicencePlateExpiryDate { get; set; }
        public List<Driver> AsignedDrivers { get; set; } = new List<Driver>();
        

        public override void GetInfo()
        {
            int assignedPercent = AsignedDrivers.Count == 0 ? 0 : 100 / 3 * AsignedDrivers.Count + 1;
            Console.WriteLine($"{Id}) [{Model}] with licence plate [{LicencePlate}] and utilized [{assignedPercent}%] ");
        }
        public void GetLicencePlateStatus()
        {
            DateOnly dateNow = DateOnly.FromDateTime( DateTime.Now );

            if ((LicencePlateExpiryDate.DayNumber - dateNow.DayNumber) < 0)
            {
                TextHelper.WriteLineInColor($"Car ID [{Id}] - Plate [{LicencePlate}] expired on {LicencePlateExpiryDate}", ConsoleColor.DarkRed);
            }
            else if ((LicencePlateExpiryDate.DayNumber - dateNow.DayNumber) < 90)
            {
                TextHelper.WriteLineInColor($"Car ID [{Id}] - Plate [{LicencePlate}] expires on {LicencePlateExpiryDate}", ConsoleColor.Yellow);
            }
            else
            {
                TextHelper.WriteLineInColor($"Car ID [{Id}] - Plate [{LicencePlate}] expires on {LicencePlateExpiryDate}", ConsoleColor.DarkGreen);
            }
        }

    }
}
