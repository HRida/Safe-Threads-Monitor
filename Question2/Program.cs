using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Question2
{
    class Program
    {
        public static int cn1 = 0;
        public static bool match = false;

        class Incrementor
        {
            public int cnt1 = 0;

            public void inc()
            {
                while (true)
                {
                    lock (this)
                    {
                        if (match)
                        {
                            Monitor.Pulse(this);
                            return;
                        }

                        cnt1++;
                        
                        cn1 = cnt1;
                        Console.WriteLine(cnt1 + "");
                        Monitor.Pulse(this);
                        Monitor.Wait(this);
                    }

                }
            }

        }


        class Decrementor
        {
            public int cnt2 = 100;

            public void dec()
            {
                while (true)
                {
                    lock (this)
                    {
                        if (cnt2 == cn1)
                        {
                            Console.WriteLine("Matching Value : 50");
                            match = true;
                            Monitor.Pulse(this);
                            return;
                        }

                        cnt2--;
                        Console.WriteLine(cnt2 + "");
                        Monitor.Pulse(this);
                        Monitor.Wait(this);

                    }

                }
            }
        }

        

        

        static void Main(string[] args)
        {
            
            Incrementor incr = new Incrementor();
            Decrementor decr = new Decrementor();
            

            Thread t1 = new Thread(incr.inc);
            Thread t2 = new Thread(decr.dec);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.ReadKey();
        
        }
    }
}
