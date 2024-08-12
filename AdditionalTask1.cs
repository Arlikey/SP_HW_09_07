using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_09_07
{
    internal class AdditionalTask1
    {
        static void Main(string[] args)
        {
            int numberOfPrinters = 3;
            PrinterDock printerDock = new PrinterDock(numberOfPrinters);

            for (int i = 1; i <= 20; i++)
            {
                int jobNumber = i;
                new Thread(() => printerDock.PrintJob(jobNumber)).Start();
            }

            Console.ReadLine();
        }
    }

    public class Printer
    {
        public void PrintJob(int jobNumber)
        {
            Console.WriteLine("Printing job {0} on printer {1}.", jobNumber, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            Console.WriteLine("Job {0} completed on printer {1}.", jobNumber, Thread.CurrentThread.ManagedThreadId);
        }
    }

    public class PrinterDock
    {
        private readonly Semaphore semaphore;
        private readonly Printer printer = new Printer();

        public PrinterDock(int numberOfPrinters)
        {
            semaphore = new Semaphore(numberOfPrinters, numberOfPrinters);
        }

        public void PrintJob(int jobNumber)
        {
            semaphore.WaitOne();
            try
            {
                printer.PrintJob(jobNumber);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
