using System;
using System.Linq;
using System.Threading;

namespace Synchronization.Examples.RaceCondition
{
    class Program
    {
		static object myLock = new object();
		static int Count;

		static void Main(string[] args)
        {
			var source = Enumerable.Range(0, 10000);

			source.AsParallel().ForAll(x => increment());

			Console.WriteLine(Count);
			Console.ReadKey();
			Console.WriteLine(Count);
			Console.ReadKey();
        }

		static void increment()
		{
			// Comment out and Uncomment this line to see the problem
			//lock(myLock) 
			{
				Count++;
			}
		}
	}
}
