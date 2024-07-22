using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace e_commerce.utils
{
    public class Debugging
    {
        public static void print(Object data){
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("********************************************");
            Console.WriteLine(data);
            Console.WriteLine("********************************************");
        }
    }
}