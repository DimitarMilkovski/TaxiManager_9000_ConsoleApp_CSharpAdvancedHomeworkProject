using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop1.Domain;
using Workshop1.Domain.Enums;
using Workshop1.Domain.Models;
using Workshop1.Helpers;

namespace Workshop1.Services
{
    public static class UserService
    {
        public static User CheckUsernameAndPassword (string username, string password)
        {

            User foundUser = Database.Users.FirstOrDefault (user => user.Username.ToLower() == username.ToLower() && user.Password == password );
            if (foundUser != null) 
            {
                return foundUser;
            }
            return null;

        }


        public static int ValidateNumber(string input)
        {
            bool isValid = int.TryParse(input, out int choice);
            if (!isValid)
            {
                TextHelper.WriteLineInColor("Invalid Input...", ConsoleColor.DarkRed);
                Console.ReadKey();
                return -1;
            }

            return choice;
        }


        public static bool CheckUsername (string username)
        {
           return Database.Users.Any(x => x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        public static Role ChooseARole()
        {
            TextHelper.WriteLineInColor("Please choose a Role for the User:", ConsoleColor.DarkBlue);
            TextHelper.WriteLineInColor("1) Administrator", ConsoleColor.Blue);
            TextHelper.WriteLineInColor("2) Manager", ConsoleColor.Blue);
            TextHelper.WriteLineInColor("3) Maintanance", ConsoleColor.Blue);
            int roleChoise = ValidateNumber(Console.ReadLine());
            if (roleChoise == -1 || roleChoise>3)
            {
                throw new Exception("Wrong Entry");
            }
            else
            {
                return (Role)roleChoise;
            }
        }

        public static Shift PickAShift()
        {
            TextHelper.WriteLineInColor("Please choose a Shift for the Driver:", ConsoleColor.DarkBlue);
            TextHelper.WriteLineInColor("1) Morning", ConsoleColor.Blue);
            TextHelper.WriteLineInColor("2) Afternoon", ConsoleColor.Blue);
            TextHelper.WriteLineInColor("3) Evening", ConsoleColor.Blue);
            int shiftChoise = ValidateNumber(Console.ReadLine());
            if (shiftChoise == -1 || shiftChoise > 3)
            {
                throw new Exception("Wrong Entry");
            }
            else
            {
                return (Shift)shiftChoise;
            }

        }

        public static void PrintUsers()
        {
            int counter = 1;
            Database.Users.ForEach(x=> 
            {

                Console.WriteLine($"{counter}) User: {x.Username} - {x.Role}");
                counter++;
                
            });
        }

        public static void MenuByRole(User currentUser)
        {
            switch (currentUser.Role)
            {
                case Role.Administrator:
                    {

                        while (true)
                        {
                            AdminService.AdminsMainMenu();
                            bool BackToMainMenu = AdminService.ChooseMenu(currentUser);
                            if (BackToMainMenu)
                            {
                                TextHelper.LeaveBlankRows(2);
                                TextHelper.WriteLineInColor("Press any key to continue...", ConsoleColor.Blue);
                                Console.ReadKey();
                                Console.Clear();
                                continue;
                            }
                            else
                            {
                                break;
                            }

                        }
                        break;
                        
                    }
                case Role.Maintanance:
                {
                    while (true)
                    {
                        MaintananceService.MaintananceMainMenu();
                        bool BackToMainMenu = MaintananceService.ChooseMenu(currentUser);
                        if (BackToMainMenu)
                        {
                            TextHelper.LeaveBlankRows(2);
                            TextHelper.WriteLineInColor("Press any key to continue...", ConsoleColor.Blue);
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            break;
                        }

                    }
                    break;
                  
                }
                case Role.Manager:
                {
                    while (true)
                    {
                        ManagerServices.ManagerMainMenu();
                        bool BackToMainMenu =  ManagerServices.ChooseMenu(currentUser);
                        if (BackToMainMenu)
                        {
                            TextHelper.LeaveBlankRows(2);
                            TextHelper.WriteLineInColor("Press any key to continue...", ConsoleColor.Blue);
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            break;
                        }

                    }
                    break;
                }

                default:
                    {
                        break;
                    }
            }
        } 

        public static void ChangePassword (User currentUser)
        {
            TextHelper.WriteLineInColor("Please enter your old password: ", ConsoleColor.Gray);
            string oldPassword = Console.ReadLine();
            if(oldPassword ==currentUser.Password)
            {
                TextHelper.WriteLineInColor("Please enter your new password: ", ConsoleColor.Green);
                string newPassword = Console.ReadLine();
                if (newPassword.Length > 5 && newPassword.Any(char.IsDigit) && newPassword != oldPassword)
                {
                    currentUser.Password = newPassword;
                    TextHelper.WriteLineInColor("Password successfully changed!", ConsoleColor.DarkGreen);
                }
                else
                {
                    throw new Exception("New Password must be longer than 5 characters, contain at least one number and be different from old password!");
                }
            }
            else
            {
                throw new Exception("Wrong Password!");
            }
        }





    }
}
