using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop1.Domain;
using Workshop1.Domain.Models;
using Workshop1.Helpers;

namespace Workshop1.Services
{
    public static class MaintananceService
    {
        public static void MaintananceMainMenu()
        {
            TextHelper.WriteLineInColor("Main Menu: ", ConsoleColor.DarkCyan);
            TextHelper.WriteLineInColor("1) List Vehicles", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("2) List Vehicles licence Plates (statuses)", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("3) Change Password", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("4) Exit", ConsoleColor.Cyan);
        }
        public static bool ChooseMenu (User currentUser)
        {
            Console.Write("->");
            int menuChoise = UserService.ValidateNumber(Console.ReadLine());
            if (menuChoise < 0 || menuChoise > 4)
                throw new Exception("Invalid Input");

            bool backToMainMenu = true;

            switch (menuChoise)
            {
                case 1:
                    {
                        Database.Cars.PrintEntities();

                        break;
                    }
                case 2:
                    {
                        Database.Cars.ForEach(car => car.GetLicencePlateStatus());
                        break;
                    }
                case 3:
                    {
                        UserService.ChangePassword(currentUser);
                        break;
                    }
                case 4:
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

    }

}
