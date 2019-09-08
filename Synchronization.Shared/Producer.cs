using System;
using System.Threading;

namespace Synchronization.Shared
{
	interface IProducer
	{
		void Produce();
		void Start(CancellationToken cancellationToken);
	}

	public class Producer: IProducer
	{
		private readonly IContainer container;
		private readonly int productionUnits;
		private readonly int productionTimeInMiliseconds;

		public Producer(IContainer container, int productionUnits = 1, int productionTimeInMiliseconds = 1)
		{
			this.container = container;
			this.productionUnits = productionUnits;
			this.productionTimeInMiliseconds = productionTimeInMiliseconds;
		}

		public void Produce()
		{
			for (int i = 0; i < productionUnits; i++)
			{
				container.Add();
			}
		}

		public void Start(CancellationToken cancellationToken)
		{
			while(!cancellationToken.IsCancellationRequested)
			{
				Produce();
				Thread.Sleep(productionTimeInMiliseconds);
			}
			Console.WriteLine($"Producer shift has been canceled");
		}
	}
}
