



using Workshop1.Domain;
using Workshop1.Domain.Models;
using Workshop1.Helpers;
using Workshop1.Services;

User currentUser = null;

while (true)
{
    Console.Clear();
    TextHelper.WriteLineInColor("****** Taxi Manager ******", ConsoleColor.DarkGreen);
    TextHelper.LeaveBlankRows(1);
    TextHelper.WriteLineInColor("Log In:", ConsoleColor.Blue);
    TextHelper.LeaveBlankRows(2);
    Console.Write("Username: --> ");
    string username = Console.ReadLine();
    TextHelper.LeaveBlankRows(1);
    Console.Write("Password: --> ");
    string password = Console.ReadLine();
    TextHelper.LeaveBlankRows(1);

    currentUser =  UserService.CheckUsernameAndPassword(username, password);
    if (currentUser == null)
    {
        TextHelper.WriteLineInColor("Login unsuccessful. Please try again", ConsoleColor.DarkRed);
        Console.ReadKey();
        Console.Clear();
        continue;
    }
    else
    {

        

        while (currentUser != null)
        {
            try
            {
                Console.Clear();
                TextHelper.LeaveBlankRows(1);
                TextHelper.WriteLineInColor($"Successful Login! Welcome {currentUser.Role} user {currentUser.Username}!", ConsoleColor.DarkGreen);
                TextHelper.LeaveBlankRows(1);

                UserService.MenuByRole(currentUser);
                TextHelper.WriteLineInColor("Exiting... Press any key to continue!", ConsoleColor.Blue);
                Console.ReadKey();
                currentUser = null;
            }

            catch (Exception ex)
            {
                TextHelper.WriteLineInColor($"{ex.Message}",ConsoleColor.DarkRed);
                Console.ReadKey();
            }
            
               
        }
        
        

    }


}









