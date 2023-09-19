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
    public static class AdminService
    {
        public static void AdminsMainMenu()
        {
            TextHelper.WriteLineInColor("Main Menu: ", ConsoleColor.DarkCyan);
            TextHelper.WriteLineInColor("1) New User", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("2) Terminate User", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("3) Change Password", ConsoleColor.Cyan);
            TextHelper.WriteLineInColor("4) Exit", ConsoleColor.Cyan);


        }

        public static bool ChooseMenu(User currentUser)
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
                        CreateNewUser();
                        
                        break;
                    }
                case 2:
                    {
                        TerminateUser(currentUser);
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

        public static void CreateNewUser()
        {
            Console.WriteLine("Please Enter a username for the new User:");
            Console.Write("->");
            string username = Console.ReadLine();

            bool isUsernameTaken = UserService.CheckUsername(username);
            if (isUsernameTaken)
            {
                throw new Exception("Username is Already Taken!");

            }
            else if (username.Length < 5)
            {
                throw new Exception("Username too short, must be at least 5 characters!");
            }
            else
            {
                Console.WriteLine("Please enter a password:");
                Console.Write("->");
                string password = Console.ReadLine();
                if (password.Length > 5 && password.Any(char.IsDigit))
                {
                    Role choosenRole = UserService.ChooseARole();
                    Database.Users.Add(new User (username, password,choosenRole));
                    TextHelper.WriteLineInColor($"Successfully created {choosenRole} user - {username}!", ConsoleColor.DarkGreen);
                    

                }
                else
                {
                    throw new Exception("Password must be at least 5 characters and must contain at least one number!");
                }
            }
        }

        public static void TerminateUser(User currnetUser)
        {
            Console.Clear();
            TextHelper.WriteLineInColor("List of all users:",ConsoleColor.Green);
            UserService.PrintUsers();
            TextHelper.WriteLineInColor("Please choose a user to terminate: ", ConsoleColor.Yellow);
            TextHelper.WriteLineInColor("->", ConsoleColor.Yellow);
            
            int choosenUser = UserService.ValidateNumber(Console.ReadLine());
            if (choosenUser <= 0 || choosenUser > Database.Users.Count) 
            {
                throw new Exception("Invalid Input!");
            }
            else
            {
                if (currnetUser == Database.Users[choosenUser - 1])
                {
                    throw new Exception("You cant terminate yourself!");
                }
                else
                {
                    TextHelper.WriteLineInColor($"Successfully terminated {Database.Users[choosenUser - 1].Role} user - {Database.Users[choosenUser - 1].Username}!", ConsoleColor.DarkGreen);
                    Database.Users.RemoveAt(choosenUser - 1);

                }
            }

        }


    }
}
