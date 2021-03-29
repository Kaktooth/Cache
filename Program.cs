using System;
using System.Runtime.InteropServices;

namespace Cache
{
    class Program
    {
        
        static void Main(string[] args)
        {
            byte[,] array = new byte[128, 4];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = 32;
                }
            }

            long freq;
            long start;
            long stop;
            double time;

            Decimal multiplier = new Decimal(1.0e9);
            Cache c = new Cache(array, 1000000);
            
            QueryPerformanceFrequency(out freq);
            QueryPerformanceCounter(out start);
            c.Bypass();
            QueryPerformanceCounter(out stop);

            time = ((double)(stop - start) / (double)freq);
            Console.WriteLine(time);

            QueryPerformanceFrequency(out freq);
            QueryPerformanceCounter(out start);
            c.ReverseBypass();
            QueryPerformanceCounter(out stop);

            time = ((double)(stop - start) / (double)freq);
            Console.WriteLine(time);


            QueryPerformanceFrequency(out freq);
            QueryPerformanceCounter(out start);
            c.RandomBypass();
            QueryPerformanceCounter(out stop);

            time = ((double)(stop - start) / (double)freq);
            Console.WriteLine(time);

            QueryPerformanceFrequency(out freq);
            QueryPerformanceCounter(out start);
            c.nBypass();
            QueryPerformanceCounter(out stop);

            time = ((double)(stop - start) / (double)freq);
            Console.WriteLine(time);


            QueryPerformanceFrequency(out freq);
            QueryPerformanceCounter(out start);
            c.nBypass();
            QueryPerformanceCounter(out stop);

            time = ((double)(stop - start) / (double)freq);
            Console.WriteLine(time);



        }
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryPerformanceFrequency(out long lpPerformanceFrequency);

        public int[,] Multiply(Cache a, Cache b)
        {
            int[,] c = new int[a.array.GetLength(0), a.array.GetLength(1)];
            for (int i = 0; i < a.array.GetLength(0); i++)
            {
                for (int j = 0; j < a.array.GetLength(1); j++)
                {
                    c[i,j] = a.array[i, j] * b.array[i, j];
                }
            }
            return c;


        }
    }
    class Cache
    {
        public byte[,] array { get; set; }
        public int offset { get; set; }
        public int BlockSize { get; set; }
        public Cache() { }
        public Cache(byte[,] array,int offset) { this.array = array; this.offset = offset; }
        public void Bypass()
        { 
           
            int Bank = 0;
            int s = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int BlockSize = 0;
               
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    
                    BlockSize += array[i, j];
                }
                if (offset < BlockSize)
                {
                    Console.WriteLine("Bigger then offset");
                }
                s = BlockSize / array.GetLength(0);         
                Bank += s;
                

            }
            var bankSum = Math.Pow(Bank, 2);

            Console.WriteLine("All banks sum: " + bankSum + "B");
        }
        public void ReverseBypass()
        {

            int Bank = 0;
            int s = 0;
            for (int i = array.GetLength(0)-1; i >= 0;i--)
            {
                int BlockSize = 0;

                for (int j = array.GetLength(1)-1; j >= 0; j--)
                {
                   
                    BlockSize += array[i, j];
                }
                if (offset < BlockSize)
                {
                    Console.WriteLine("Bigger then offset");
                }
                s = BlockSize / array.GetLength(0);
                Bank += s;


            }
            var bankSum = Math.Pow(Bank, 2);

            Console.WriteLine("All banks sum: " + bankSum + "B");
        }
        public void RandomBypass()
        {
            Random r = new Random();
          
            int Bank = 0;
            int s = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int BlockSize = 0;
                int rand = r.Next(0, array.GetLength(0));
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    BlockSize += array[rand, j]; 
                }
                if (offset < BlockSize)
                {
                    Console.WriteLine("Bigger then offset");
                }
                
                s = BlockSize / array.GetLength(0);
                Bank += s;


            }
            var bankSum = Math.Pow(Bank, 2);

            Console.WriteLine("All banks sum: " + bankSum + "B");
        }
        
        public void nBypass()
        {

            int Bank = 0;
            int s = 0;
            for (int i = 0; i < array.GetLength(1); i++)
            {
                int BlockSize = 0;

                for (int j = 0; j < array.GetLength(0); j++)
                {
                  
                    BlockSize += array[j, i];
                }
                if (offset < BlockSize)
                {
                    Console.WriteLine("Bigger then offset");
                }
                s = BlockSize / array.GetLength(0);
                Bank += s;


            }
            var bankSum = Math.Pow(Bank, 2);

            Console.WriteLine("All banks sum: " + bankSum + "B");
        }
    }
}
