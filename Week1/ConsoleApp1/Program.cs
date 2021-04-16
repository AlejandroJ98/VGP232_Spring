using System;

namespace ConsoleApp1
{
    class Program
    {
        static string name = "Erick";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RenameName(ref name, "John");
            Console.WriteLine("original name is: {0}", name);
        }

        static void RenameName(ref string originalName, string newName)
        {
            originalName = newName;
            Console.WriteLine("The original name is:{0}", originalName);
        }
    }
}
