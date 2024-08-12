using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_09_07
{
    internal class AdditionalTask3
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                for (int i = 1; i <= 100; i += 2)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(100);
                }
            }).Start();
            new Thread(() =>
            {
                for (int i = 2; i <= 100; i += 2)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(100);
                }
            }).Start();

            Console.ReadLine();
        }
    }
}
