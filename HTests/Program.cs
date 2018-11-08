using System;
using System.Numerics;
using GVM.Src;
using GVM.Src.Types;

namespace HTests
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                foreach (var item in BitConverter.GetBytes(-10))
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
                foreach (var item in BitConverter.GetBytes(-500))
                {
                    Console.WriteLine(item);
                }
                Console.ReadKey();
                Console.WriteLine("---------------");
            }
        }
    }
}
