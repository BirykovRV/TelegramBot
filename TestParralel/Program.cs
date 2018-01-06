using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParralel
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(async () =>
            {
                await Factorial2(10);

            }, async () =>
            {
                await Factorial(5);
            });

            Console.ReadKey();
        }       

        public static async Task Factorial(int x)
        {
            await Task.Delay(100);
            Console.WriteLine(x);
        }

        public static async Task Factorial2(int x)
        {
            await Task.Delay(100);
            Console.WriteLine(x);
        }
    }
}
