using System;
using System.Threading;

namespace Synchronization.Shared
{
	public interface IContainer
	{
		void Add();
		void Remove();
	}

	public class QuantityContainer : IContainer
	{
		public int Count { get; private set; }
		public int MaxCapacity { get; private set; }

		public QuantityContainer(int maxCapacity)
		{
			Count = 0;
			MaxCapacity = maxCapacity;
		}

		public void Add()
		{
			if (Count < MaxCapacity)
			{
				Count++;
			}
		}

		public void Remove()
		{
			if (Count > 0)
			{
				Count--;
			}
		}

		public void MonitorCount(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				if (Count < 0 || MaxCapacity < Count)
				{
					throw new ApplicationException($"Count is outside its limits {Count}");
				}
			}
			Console.WriteLine("Monitoring has been canceled");
		}
	}
}
