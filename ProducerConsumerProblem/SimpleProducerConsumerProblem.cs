using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ProducerConsumerProblem
{
    public class SimpleProducerConsumerProblem
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
                while (true)
                {
                    bin.Produce();
                    Thread.Sleep(new Random().Next(10)); // Simulate producing takes a random time.
                }
            }
        }

        class Consumer
        {
            private SharedResource bin;

            public Consumer(SharedResource bin)
            {
                this.bin = bin;
            }

            public void Start()
            {
                while (true)
                {
                    bin.Consume();
                    Thread.Sleep(new Random().Next(10)); // Simulate consuming takes a random time.
                }
            }
        }

        class SharedResource
        {
            private int maxCapacity;
            private int count;

            public SharedResource(int maxCapacity)
            {
                this.maxCapacity = maxCapacity;
                this.count = 0;
            }

            public void Produce()
            {
                if (count < maxCapacity)
                {
                    count++;
                    if (count > maxCapacity)
                    {
                        Console.WriteLine("PRODUCED - ERROR........................" + count);
                        throw new Exception();
                    }
                }
                Console.WriteLine("PRODUCED: " + count);
            }

            public void Consume()
            {
                if (count > 0)
                {
                    count--;
                    if (count < 0)
                    {
                        Console.WriteLine("CONSUMED - ERROR........................" + count);
                        throw new Exception();
                    }
                }
                Console.WriteLine("CONSUMED: " + count);
            }
        }

        public void Test()
        {
            SharedResource bin = new SharedResource(5);

            Producer producer = new Producer(bin);
            Thread tProducer = new Thread(new ThreadStart(producer.Start));
            tProducer.Start();

            Consumer consumer1 = new Consumer(bin);
            Consumer consumer2 = new Consumer(bin);
            Consumer consumer3 = new Consumer(bin);
            Thread tConsumer1 = new Thread(new ThreadStart(consumer1.Start));
            Thread tConsumer2 = new Thread(new ThreadStart(consumer2.Start));
            Thread tConsumer3 = new Thread(new ThreadStart(consumer3.Start));
            tConsumer1.Start();
            tConsumer2.Start();
            tConsumer3.Start();
        }

    }
}
