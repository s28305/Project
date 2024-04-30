using System;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = LinqTasks.Task6();
           foreach (var item in t)
            {
               
                // Print values
             Console.WriteLine(item);
           }

        }
    }
}
