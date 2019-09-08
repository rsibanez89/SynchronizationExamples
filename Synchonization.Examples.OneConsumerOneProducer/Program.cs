using Synchronization.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization.Examples.ClassicProducerConsumer
{
	class Program
	{
		static void Main(string[] args)
		{
			TheClasicProduceConsumerProblem();
		}

		static void TheClasicProduceConsumerProblem()
		{
			var sharedContainer = new QuantityContainer(5);

			var cancelationToken = new CancellationTokenSource();
			for (int i = 0; i < 10; i++)
			{
				Task.Factory.StartNew(() => new Producer(sharedContainer).Start(cancelationToken.Token));
				Task.Factory.StartNew(() => new Consumer(sharedContainer).Start(cancelationToken.Token));
			}
			Task.Factory.StartNew(() => sharedContainer.MonitorCount(cancelationToken.Token));

			Console.WriteLine("Press any key to cancel");
			Console.ReadKey();
			cancelationToken.Cancel();
		}
	}
}
