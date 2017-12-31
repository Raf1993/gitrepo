
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
namespace CodeFromPromelaModel
{
    public class Program
    {
        private static int x = 2;
        private static object staticObjLockA = new Object();
        private static object staticObjLockB = new Object();

        private static void Main(string[] args)
        {
            // Initialize thread with address of DoWork1
            Thread thread1 = new Thread(DoWork1);

            // Initilaize thread with address of DoWork2
            Task thread2 = new Task(DoWork2);

            // Start the Threads.
            thread1.Start();
            thread2.Start();

        //    thread1.Join();
        //    thread2.Join();

            // This statement will never be executed.
            Console.WriteLine("Done Processing...");

        }

        private static void DoWork1()
        {
            x++;
            lock (staticObjLockA)
            {
                Console.WriteLine("Trying to acquire lock on staticObjLockB");

                // Sleep to yield.
                Thread.Sleep(1000);
                lock (staticObjLockB)
                {
                    // This block will never be executed.
                    Console.WriteLine("In DoWork1 Critical Section.");
                    // Access some shared resource here.
                }
            }
        }

        private static void DoWork2()
        {
            x++;
            lock (staticObjLockB)
            {
                Console.WriteLine("Trying to acquire lock on staticObjLockA");
                lock (staticObjLockA)
                {
                    // This block will never be executed.
                    Console.WriteLine("In DoWork2 Critical Section.");
                    // Access some shared resource here.
                }
            }
        }
    }


 

}
