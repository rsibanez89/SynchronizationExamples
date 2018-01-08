using System;
using System.Threading;

namespace ProducerConsumerProblem
{
    

    class Program
    {
        static void Main(string[] args)
        {
            //new SimpleRaceConditionProblem().Test();
            //new SimpleRaceConditionProblemV2().Test();
            new SimpleProducerConsumerProblem().Test();

            Console.ReadKey();
        }
    }
}
