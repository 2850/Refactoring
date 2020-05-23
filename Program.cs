using System;

namespace Refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DateTime d1 = new DateTime(2020,05,23);
            nextDateUpdate(d1);
            Console.WriteLine("d1 after nextDay: " + d1);

            DateTime d2 = new DateTime(2020,05,23);
            nextDateReplace(d2);
            Console.WriteLine("d2 after nextDay: " + d2);
        }

        private static void nextDateUpdate (DateTime arg){
            arg = arg.AddDays(1);
            Console.WriteLine("arg in nextDay: " + arg);
        }

        private static void nextDateReplace (DateTime arg){
            
            arg = new DateTime(arg.Year,arg.Month,arg.Day + 1);
            Console.WriteLine("arg in nextDay:" + arg);
        }
    }
}
