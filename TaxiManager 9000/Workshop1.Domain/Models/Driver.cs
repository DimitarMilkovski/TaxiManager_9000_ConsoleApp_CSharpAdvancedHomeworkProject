using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Workshop1.Domain.Enums;
using Workshop1.Helpers;

namespace Workshop1.Domain.Models
{
    public class Driver : BaseEntity
    {
        private static int _driverCounter = 1;

        public Driver(string firstName, string lastName, Shift shift, string licence, DateOnly licenceExpiryDate, Car car )
        {
            Id = _driverCounter;
            FirstName = firstName;
            LastName = lastName;
            Shift = shift;
            Licence = licence;
            LicenceExpiryDate = licenceExpiryDate;
            Car = car;
            _driverCounter++;
        }
        public Driver(string firstName, string lastName, Shift shift, string licence, DateOnly licenceExpiryDate)
        {
            Id = _driverCounter;
            FirstName = firstName;
            LastName = lastName;
            Shift = shift;
            Licence = licence;
            LicenceExpiryDate = licenceExpiryDate;
            _driverCounter++;
        }
        private Car _car;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Shift Shift { get; set; } = Shift.Unassigned;
        public Car Car { get; set; }
        public string Licence { get; set; }
        public DateOnly LicenceExpiryDate { get; set; }


        public override void GetInfo()
        {
            if(Car?.Model == null || Shift == Shift.Unassigned) 
            {
                Console.WriteLine($"{Id}) {FirstName} {LastName} - Unassigned");
            }

            else
            {
                Console.WriteLine($"{Id}) {FirstName} {LastName} Driving in the {Shift} shift with a {Car.Model} car.");

            }
        }
        public void GetLicenceStatus()
        {
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);

            if ((LicenceExpiryDate.DayNumber - dateNow.DayNumber) < 0)
            {
                TextHelper.WriteLineInColor($"Driver [{FirstName} {LastName}] with Licence [{Licence}] expired on {LicenceExpiryDate}", ConsoleColor.DarkRed);
            }
            else if ((LicenceExpiryDate.DayNumber - dateNow.DayNumber) < 90)
            {
                TextHelper.WriteLineInColor($"Driver [{FirstName} {LastName}] with Licence [{Licence}] expires on {LicenceExpiryDate}", ConsoleColor.Yellow);
            }
            else
            {
                TextHelper.WriteLineInColor($"Driver [{FirstName} {LastName}] with Licence [{Licence}] expires on {LicenceExpiryDate}", ConsoleColor.DarkGreen);
            }
        }
    }
}
