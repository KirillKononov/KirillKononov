using System;
using System.Diagnostics;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            var classes = new C[100000];

            var memoryC = Process.GetCurrentProcess().PrivateMemorySize64;
            for (int j = 0; j < classes.Length; ++j)
            {
                classes[j] = new C(rnd.Next());
            }
            memoryC = Process.GetCurrentProcess().PrivateMemorySize64 - memoryC;

            var structs = new S[100000];

            var memoryS = Process.GetCurrentProcess().PrivateMemorySize64;
            for (int j = 0; j < structs.Length; ++j)
            {
                structs[j] = new S(rnd.Next());
            }
            memoryS = Process.GetCurrentProcess().PrivateMemorySize64 - memoryS;

            Console.WriteLine($"Memory for classes initialization: {memoryC}");
            Console.WriteLine($"Memory for structs initialization: {memoryS}");
            Console.WriteLine("For structs initialization we use much less memory than for classes ");
            Console.WriteLine("if we work with the huge arrays (more than 4300 elements)");


            var time = new Stopwatch();
            time.Start();
            Array.Sort(classes, (x, y) => x.i.CompareTo(y.i));
            time.Stop();
            Console.WriteLine($"Execution time of classes sort: {time.Elapsed}");

            time.Restart();
            Array.Sort(structs, (x, y) => x.i.CompareTo(y.i));
            time.Stop();
            Console.WriteLine($"Execution time of structs sort: {time.Elapsed}");
            Console.ReadLine();
        }
    }
}
