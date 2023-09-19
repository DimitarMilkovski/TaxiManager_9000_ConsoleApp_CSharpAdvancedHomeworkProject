using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop1.Helpers
{
    public class TextHelper
    {
        public static void WriteLineInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void LeaveBlankRows(int rows)
        {
            for(int i = 0; i < rows; i++)
            {
                Console.WriteLine();
            }
        }

       

        
    }
}
