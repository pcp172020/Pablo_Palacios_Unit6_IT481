using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pablo_Palacios_Unit6_IT481;

namespace Pablo_Palacios_Unit6_IT481
{
    class Scenario
    {
        static Customer cust;
        static int items = 0;
        static int numberOfItems;
        static int controlItemNumber;

        public Scenario(int r, int c)
        {
            Console.WriteLine(r + " dressing rooms " +
                " for " + c + " customers.");

            controlItemNumber = 0;
            numberOfItems = 0;

        }

        static void Main(string[] args)
        {
            //ClothingItems = 0 will indicate the use of a random number. 
            //ClothingItems = 1 - 30 will allow for load testing by forcing a specific number of items. 
            Console.WriteLine("What ClothingItems value do you want? (0 = random)");
            controlItemNumber = Int32.Parse(Console.ReadLine());

            //Set the number of custoemrs
            Console.WriteLine("How many customers do you want? ");
            int numberOfCustomers = Int32.Parse(Console.ReadLine());
            Console.WriteLine("There are " + numberOfCustomers +
                " total customers");

            //Set the number of dressing rooms
            Console.WriteLine("How many dressing rooms do you want? ");
            int totalRooms = Int32.Parse(Console.ReadLine());

            //Start the scenario for testing
            Scenario s = new Scenario(totalRooms, numberOfCustomers);

            //Create the dressing rooms object with number of rooms
            DressingRooms dr = new DressingRooms(totalRooms);

            List<Task> tasks = new List<Task>();

            //Loop through the customers and create threads
            for (int i = 0; i < numberOfCustomers; i++)
            {
                //Create the customer object
                cust = new Customer(controlItemNumber);

                //Get the number of items
                numberOfItems = cust.getNumberOfItems();

                //Track total number of items
                items += numberOfItems;

                //Start async request room
                tasks.Add(Task.Factory.StartNew(async () =>{
                    await dr.RequestRoom(cust);
                }));

            }

            Task.WaitAll(tasks.ToArray());


            Console.WriteLine("Average Run time in milliseconds {0} ", dr.getRunTime() / numberOfCustomers);
            Console.WriteLine("Average Wait time in milliseconds {0} ", dr.getWaitTime() / numberOfCustomers);
            Console.WriteLine("Total customers is {0}", numberOfCustomers);
            int averageItemsPerCustomer =
                    items / numberOfCustomers;
            Console.WriteLine("Average number of items was: " +
                averageItemsPerCustomer);
            Console.Read();
        }

    }
}