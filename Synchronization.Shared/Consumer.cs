using System;
using System.Threading;

namespace Synchronization.Shared
{
	public interface IConsumer
	{
		void Consume();
		void Start(CancellationToken cancellationToken);
	}

	public class Consumer : IConsumer
	{
		private readonly IContainer container;
		private readonly int consumptionUnits;
		private readonly int consumptionTimeInMiliseconds;

		public Consumer(IContainer container, int consumptionUnits = 1, int consumptionTimeInMiliseconds = 1)
		{
			this.container = container;
			this.consumptionUnits = consumptionUnits;
			this.consumptionTimeInMiliseconds = consumptionTimeInMiliseconds;
		}

		public void Consume()
		{
			for (int i = 0; i < consumptionUnits; i++)
			{
				container.Remove();
			}
		}

		public void Start(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				Consume();
				Thread.Sleep(consumptionTimeInMiliseconds);
			}
			Console.WriteLine($"Consumer shift has been canceled");
		}
	}
}
