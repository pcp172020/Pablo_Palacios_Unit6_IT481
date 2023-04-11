using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pablo_Palacios_Unit6_IT481
{
    class DressingRooms
    {

        int rooms;
        Semaphore semaphore;
        long waitTimer;
        long runTimer;


        public DressingRooms()
        {
            rooms = 3;
            semaphore = new Semaphore(rooms, rooms);
        }

        public DressingRooms(int r)
        {

            rooms = r;
            //Set the semaphore object
            semaphore = new Semaphore(rooms, rooms);

        }

        public async Task RequestRoom(Customer c)
        {
            Stopwatch stopWatch = new Stopwatch();
            //Waiting thread
            Console.WriteLine("Customer is waitin");

            //Start the timer
            stopWatch.Start();
            semaphore.WaitOne();
            //Stop the wait timer
            stopWatch.Stop();
            //Get the time elapsed for waiting
            waitTimer += stopWatch.ElapsedTicks;

            int roomWaitTime = GetRandomNumber(1, 3);
            //Start the timer
            stopWatch.Start();
            Thread.Sleep((roomWaitTime * c.getNumberOfItems()));
            //Stop the wait timer
            stopWatch.Stop();
            //Get elapsed run time
            runTimer += stopWatch.ElapsedTicks;

            Console.WriteLine("Customer finished trying on items in room");
            semaphore.Release();


        }

        public long getWaitTime()
        {
            return waitTimer;
        }

        public long getRunTime()
        {
            return runTimer;
        }


        //Random Number Method
        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }
    }
}
