using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_09_07
{
    internal class AdditionalTask2
    {
        static object locker = new object();
        static Random random = new Random();
        static void Main()
        {
            List<double> items = new List<double>();
            

            AutoResetEvent isDone = new AutoResetEvent(false);
            double totalWeight = 0.0;
            const double weightLimit = 20.0;

            var serverTask = Task.Run(() =>
            {
                while (true)
                {
                    lock (locker)
                    {
                        if (totalWeight > weightLimit)
                            break;
                    }
                    Thread.Sleep(100); 
                }

                isDone.Set();
            });

            var random = new Random();
            var familyMembers = new[]
            {
            Task.Run(() => TossInSuitcase(items, isDone, "Mother", ref totalWeight, weightLimit)),
            Task.Run(() => TossInSuitcase(items, isDone, "Father", ref totalWeight, weightLimit)),
            Task.Run(() => TossInSuitcase(items, isDone, "Son", ref totalWeight, weightLimit))
        };

            isDone.WaitOne();

            Task.WhenAll(familyMembers).Wait();

            Console.WriteLine($"Filling suitcase is finished! Total weight: {totalWeight}");
        }

        static void TossInSuitcase(List<double> items, AutoResetEvent isDone, string familyMember, ref double totalWeight, double weightLimit)
        {
            int itemCount = 0;
            while (!isDone.WaitOne(0))
            {
                double itemWeight = random.NextDouble() * (4.5 - 0.1) + 0.1;
                lock (locker)
                {
                    if (totalWeight + itemWeight > weightLimit)
                    {
                        isDone.Set(); 
                        break;
                    }

                    items.Add(itemWeight);
                    totalWeight += itemWeight;
                }

                itemCount++;
                Console.WriteLine($"{familyMember} add {itemWeight:F2} kg in suitcase.");
                Thread.Sleep(random.Next(100, 500));
            }

            Console.WriteLine($"{familyMember} added {itemCount} items.");
        }
    }
}
