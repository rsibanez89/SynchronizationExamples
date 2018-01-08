using System;
using System.Threading;

namespace ProducerConsumer
{
    // For this particular example, there is no need to use locks as there is just 1 producer and 1 consumer and int operations are atomic in C#.
    // In Java int operations are not atomics, that means that count++; and count--; can be executed at the same time and

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
                Thread.Sleep(new Random().Next(1000)); // Simulate producing takes a random time.
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
            while(true)
            {
                bin.Consume();
                Thread.Sleep(new Random().Next(1000)); // Simulate consuming takes a random time.
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
            lock(this)
            {
                if (count < maxCapacity)
                {
                    count++;
                    Monitor.Pulse(this); // Notify consumer that there is something to consume.
                }
                else
                {
                    Console.WriteLine("PRODUCER SLEEPING...");
                    Monitor.Wait(this); // MaxCapacity reached, sleep until get notified and we can produce again.
                }  
            }
            Console.WriteLine("PRODUCED: " + count);
        }

        public void Consume()
        {
            lock (this)
            {
                if (count > 0)
                {
                    count--;
                    Monitor.Pulse(this); // Notify producer that there is space to keep producing consume.
                }
                else
                {
                    Console.WriteLine("CONSUMER SLEEPING...");
                    Monitor.Wait(this); // Nothing to consume, sleep until get notified and we can consume again.
                }
            }
            Console.WriteLine("CONSUMED: " + count);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SharedResource bin = new SharedResource(10);
            Producer producer = new Producer(bin);
            Consumer consumer = new Consumer(bin);
            Thread tProducer = new Thread(new ThreadStart(producer.Start));
            Thread tConsumer = new Thread(new ThreadStart(consumer.Start));
            tProducer.Start();
            tConsumer.Start();

            Console.ReadKey();
        }
    }
}
