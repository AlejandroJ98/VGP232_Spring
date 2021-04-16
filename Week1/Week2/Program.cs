using System;

namespace Week2
{
    class Program
    {
       public enum DayOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Size

        }

        public enum Fruits
        {
            Banana,
            Apple,
            Watermelon,
            Size
        }

        static void RunningSum(int left, ref int total)
        {
            total += left;
        }

        static void OutSum(int left, int right, out int total)
        {
            total = left + right;
        }

        static int SumParams(params int[] numbers)
        {
            int total = 0;
            for(int i = 0; i < numbers.Length; ++i)
            {
                total += numbers[i];
            }
            return total;
        }

        //Optionals

        static void PrintAName(string name = "Default")
        {
            Console.WriteLine(name);
        }

        //Named Parameters
        static void SetConfiguration(bool fullScreenEnabled, ref int width, out int height)
        {
            Console.WriteLine("Configuration set.");
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello world");

            //Fruits myFruit = Fruits.Apple;

            //string myStringFruit = "Apple";

            //Fruits myFruit_2 = Enum.Parse(typeof(Fruits), myStringFruit);

            //DayOfWeek myFruit_3;
            //if(Enum.TryParse<Fruits>(myStringFruit, out myFruit_3))
            //{

            //}
            //else
            //{

            //}

            int total = 3;
            RunningSum(7, ref total);
            Console.WriteLine(total); 

            int total_2;
            OutSum(4, 2, out total_2);
            Console.WriteLine("Out Total:{0}", total_2);

            int[] Numbers = { 1, 2, 3, 4, 5, 6, 7 };
            //Sum(Numbers);

            SumParams(1, 2, 3, 4, 5, 6, 7);

            PrintAName();
            PrintAName("Erick");

            SetConfiguration(height: out total_2, fullScreenEnabled: false, width: ref total);
        }
    }
}
