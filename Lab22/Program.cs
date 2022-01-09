using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива:");
            int a = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object,int[]>(Method1);
            Task<int[]> task1 = new Task<int[]>(func1, a);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(Method2);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(Method3);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            task1.Start();

            Console.ReadKey();
        }
        static int[] Method1(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = rand.Next(0, 100);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }
        static int Method2(Task<int[]> task)
        {
            int[] array = task.Result;
            int s = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                s += array[i];
            }
            
            Console.WriteLine($"Сумма чисел массива = {s}");
            return s;
        }
        static int Method3(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            for (int i = 0; i < array.Count(); i++)
            {
                if (array[i]>max)
                {
                    max = array[i];
                }
            }
            Console.WriteLine($"Максимальное число массива = {max}");
            return max;
        }
    }
}
