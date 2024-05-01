using System;
using System.Collections.Generic;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        { 
            //Task1
            var t1 = LinqTasks.Task1(); 
            TaskPrintElements(t1);
            
            //Task2
            var t2 = LinqTasks.Task2(); 
            TaskPrintElements(t2);
            
            //Task3
            var t3 = LinqTasks.Task3(); 
            Console.WriteLine(t3 + "\n");
            
            //Task4
            var t4 = LinqTasks.Task4(); 
            TaskPrintElements(t4);
            
            //Task5
            var t5 = LinqTasks.Task5(); 
            TaskPrintElements(t5);
            
            //Task6
            var t6 = LinqTasks.Task6(); 
            TaskPrintElements(t6);
            
            //Task7
            var t7 = LinqTasks.Task7(); 
            TaskPrintElements(t7);
            
            //Task8
            var t8 = LinqTasks.Task8(); 
            Console.WriteLine(t8 + "\n");
            
            //Task9
            var t9 = LinqTasks.Task9(); 
            Console.WriteLine(t9 + "\n");
            
            //Task10
            var t10 = LinqTasks.Task10(); 
            TaskPrintElements(t10);
            
            //Task11
            var t11 = LinqTasks.Task11(); 
            TaskPrintElements(t11);
            
            //Task12
            var t12 = LinqTasks.Task12(); 
            TaskPrintElements(t12);
            
            //Task13
            var els = new[] {1, 2, 3, 2, 1, 3, 1};
            var t13 = LinqTasks.Task13(els); 
            Console.WriteLine(t13 + "\n");
            
            //Task14
            var t14 = LinqTasks.Task14(); 
            foreach (var dept in t14)
            {
                Console.WriteLine(dept);
            }
            Console.WriteLine();
            
            //Task15
            var t15 = LinqTasks.Task15(); 
            TaskPrintElements(t15);
            
            //Task16
            var t16 = LinqTasks.Task16(); 
            TaskPrintElements(t16);
           
        }

        private static void TaskPrintElements(IEnumerable<object> objects)
        {
            foreach (var o in objects)
            {
                Console.WriteLine(o); 
            }
            Console.WriteLine();
            
        }
    }
}
