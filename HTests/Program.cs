using System;
using System.Diagnostics;
using System.Numerics;
using GVM.Src;
using GVM.Src.Types;

namespace HTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var tick = 1000_000;
            Stopwatch s = new Stopwatch();
            s.Start();
            var p = new BigInteger(Int256.MaxValue.ToByteArray());
            for (int i = 0; i < tick; i++)
            {
                p--;
            }
            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);
            s.Reset();
            s.Start();
            Int256 pp = Int256.MaxValue;
            for (int i = 0; i < tick; i++)
            {
                pp--;
            }
            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
