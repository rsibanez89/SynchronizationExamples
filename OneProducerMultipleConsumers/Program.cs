using System;
using System.Linq;
using System.Threading;

namespace OneProducerMultipleConsumers
{
    public class Program
    {

        class Producer
        {
            private SharedResource bin;

            public Producer(SharedResource bin)
            {
                this.bin = bin;
            }

            public void Start()
            {
                for (int i = 0; i < 100; i++)
                {
                    bin.Produce();
                }
            }
        }

        class SharedResource
        {
            private int count;
            public int BinSize { get { return count; } }

            public SharedResource()
            {
                this.count = 0;
            }

            public void Produce()
            {
                count++;
            }
        }


        static void Main(string[] args)
        {
            SharedResource bin = new SharedResource();
            for (int i = 0; i < 100; i++)
            {
                Producer producer = new Producer(bin);
                Thread tProducer = new Thread(new ThreadStart(producer.Start));
                tProducer.Start();
            }

            Console.ReadKey();
            Console.WriteLine(bin.BinSize);
            Console.ReadKey();
            Console.WriteLine(bin.BinSize);
            Console.ReadKey();
        }
    }
}
