using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProducerConsumerProblem
{
    public class SimpleRaceConditionProblem
    {
        static object myLock = new object();

        static int count;

        static void increment()
        {
            // Comment out and Uncomment this line to see the problem
            //lock(myLock) 
            {
                count++;
            }
        }

        public void Test()
        {
            Console.WriteLine(count);

            var source = Enumerable.Range(0, 10000);

            source.AsParallel().ForAll(x => increment());

            Console.WriteLine(count);
            Console.ReadKey();
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
