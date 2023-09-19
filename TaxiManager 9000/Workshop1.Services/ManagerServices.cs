using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop1.Domain.Models;
using Workshop1.Domain;
using Workshop1.Helpers;
using Workshop1.Domain.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Workshop1.Services
{
    public static class ManagerServices
    {
        public static void ManagerMainMenu()
        {
            TextHelper.WriteLineInColor("Main Menu: ", ConsoleColor.DarkCyan);
            TextHelper.WriteLineInColor("1) List Drivers", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("2) List drivers licences (statuses)", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("3) Driver Manager", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("4) Change Password", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("5) Exit", ConsoleColor.Cyan);
        }
        public static bool ChooseMenu(User currentUser)
        {
            Console.Write("->");
            int menuChoise = UserService.ValidateNumber(Console.ReadLine());
            if (menuChoise < 0 || menuChoise > 5)
                throw new Exception("Invalid Input");

            bool backToMainMenu = true;

            switch (menuChoise)
            {
                case 1:
                    {
                        Database.Drivers.PrintEntities();
                        
                        break;
                    }
                case 2:
                    {
                        Database.Drivers.ForEach(driver => driver.GetLicenceStatus());
                        
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        TextHelper.WriteLineInColor("Driver Manager: ", ConsoleColor.DarkCyan);
                        TextHelper.WriteLineInColor("1) Assign Unassigned Drivers", ConsoleColor.Cyan);
                        TextHelper.WriteLineInColor("2) Unasign Assigned Drivers", ConsoleColor.Cyan);
                        TextHelper.WriteLineInColor("3) Back To Main Menu", ConsoleColor.Cyan);

                        int subMenuChoise = UserService.ValidateNumber(Console.ReadLine());
                        if (subMenuChoise == 1)
                        {
                            AssignDrivers();
                        }
                        else if (subMenuChoise == 2)
                        {
                            UnasignDriver();
                        }
                        else if (subMenuChoise == 3)
                        {
                            
                            break;
                        }
                        else
                        {
                            throw new Exception("Invalid Input!");
                        }
                        
                        break;
                    }
                case 4:
                    {
                        UserService.ChangePassword(currentUser);
                        
                        break;
                    }
                case 5:
                    {
                        backToMainMenu = false;
                        break;
                    }
                default:
                    {
                        backToMainMenu = false;
                        break;
                    }

            }
            return backToMainMenu;
        }

        public static void AssignDrivers()
        {
            Console.Clear();

            

            List<Driver> unassignedDrivers = Database.Drivers.Where(x => x.Shift == Shift.Unassigned && x.Car == null).ToList();
            int counterForDrivers = 1;
            Driver choosenDriver = null;


            if (unassignedDrivers.Count > 0)
            {

                TextHelper.WriteLineInColor("List of all unassigned Drivers:", ConsoleColor.DarkBlue);
                foreach (Driver driver in unassignedDrivers)
                {
                    TextHelper.WriteLineInColor($"{counterForDrivers}) {driver.FirstName} {driver.LastName} - Unassigned", ConsoleColor.Blue);
                    counterForDrivers++;
                }
                Console.WriteLine();
            }
            else
            {
                throw new Exception("There is no available Drivers!");
            }



            TextHelper.WriteLineInColor("Please choose a Driver to Assign to: ", ConsoleColor.Yellow);
            TextHelper.WriteLineInColor("->", ConsoleColor.Yellow);
            int choosenDriverNumber = UserService.ValidateNumber(Console.ReadLine());
            if (choosenDriverNumber < 1 || choosenDriverNumber > unassignedDrivers.Count)
            {
                throw new Exception("Invalid input!");
            }
            else
            {
                choosenDriver = unassignedDrivers[choosenDriverNumber - 1];
                Shift shiftToAssign = UserService.PickAShift();


                DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
                List<Car> availableCars = Database.Cars.Where(car => (car.AsignedDrivers.Where(driver => driver.Shift == shiftToAssign).ToList().Count == 0 && (car.LicencePlateExpiryDate.DayNumber - dateNow.DayNumber) > 0)).ToList();

                if (availableCars.Count ==0)
                {
                    throw new Exception("There is no available Cars!");
                }
                else
                {

                    TextHelper.WriteLineInColor("List of all available Cars:", ConsoleColor.DarkBlue);
                    int counterForCars = 1;
                    foreach (Car car in availableCars)
                    {
                        TextHelper.WriteLineInColor($"{counterForCars}) {car.Model} with licence plate: {car.LicencePlate}", ConsoleColor.Blue);
                        counterForCars++;
                    }
                    Console.WriteLine();


                    TextHelper.WriteLineInColor("Please choose a Car for the driver: ", ConsoleColor.Yellow);
                    TextHelper.WriteLineInColor("->", ConsoleColor.Yellow);
                    int choosenCarNumber = UserService.ValidateNumber(Console.ReadLine());
                    if (choosenCarNumber < 1 || choosenCarNumber > availableCars.Count)
                    {
                        throw new Exception("Invalid input!");
                    }
                    else
                    {
                        choosenDriver.Car = availableCars[choosenCarNumber - 1];
                        choosenDriver.Shift = shiftToAssign;
                        choosenDriver.Car.AsignedDrivers.Add(choosenDriver);

                        TextHelper.WriteLineInColor("Driver successfully assigned!", ConsoleColor.DarkGreen);


                    }
                }

            }
        }


        public static void UnasignDriver()
        {
            Console.Clear();
            List<Driver> assignedDrivers = Database.Drivers.Where(x => x.Car != null).ToList();
            int counterForDrivers = 1;
            Driver choosenDriver = null;

            if (assignedDrivers.Count > 0)
            {

                TextHelper.WriteLineInColor("List of all assigned Drivers:", ConsoleColor.DarkBlue);

                foreach (Driver driver in assignedDrivers)
                {
                    TextHelper.WriteLineInColor($"{counterForDrivers}) {driver.FirstName} {driver.LastName} - assigned", ConsoleColor.Blue);
                    counterForDrivers++;
                }
                Console.WriteLine();
            }
            else
            {
                throw new Exception("There is no driver to unasign!");
            }


            TextHelper.WriteLineInColor("Please choose a Driver to unasign: ", ConsoleColor.Yellow);
            TextHelper.WriteLineInColor("->", ConsoleColor.Yellow);
            int choosenDriverNumber = UserService.ValidateNumber(Console.ReadLine());
            if (choosenDriverNumber < 1 || choosenDriverNumber > assignedDrivers.Count)
            {
                throw new Exception("Invalid input!");
            }
            else
            {
                choosenDriver = assignedDrivers[choosenDriverNumber - 1];
                choosenDriver.Car.AsignedDrivers.Remove(choosenDriver);
                choosenDriver.Car = null;
                choosenDriver.Shift = Shift.Unassigned;

                TextHelper.WriteLineInColor("Driver successfully unsigned!", ConsoleColor.Red);
            }
        }
    }
}
