using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static program.Utilities;
using System.Diagnostics;
namespace program
{
    class Program
    {
        static List<List<int>> ReadFromFile(StreamReader stream)
        {
            List<List<int>> list = new List<List<int>>();
            while (!stream.EndOfStream)
                list.Add(stream.ReadLine().Split(' ').Select(int.Parse).ToList()); // в цикле парсим каждую строку входного файла, которую затем преобразуем в один элемент массива, повторяем до конца файла
            return list; // возвращаем полученный массив
        }
        static bool SequentialSearch(List<int> array, int element, out int index, out int comparisons)
        {
            comparisons = 0;
            for (int i = 0; i < array.Count; i++)
            {
                comparisons++; // считаем количество сравнений
                if (array[i] == element) // проходим по всем элементам массива и сравниваем их с искомым, если находим, то запоминаем индекс и возвращаем положительный ответ
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false; // если мы не смогли найти искомый элемент, возвращаем false и индекс -1
        }
        static bool BinarySearch(List<int> array, int element, out int index, out int comparisons)
        {
            int l = 0; // левый указатель
            int r = array.Count - 1; // правый указатель
            comparisons = 0;
            while (l <= r) // пока между указателями расстояние не отрицательно
            {
                int c = l + (r - l) / 2; // находим центральный элемент
                comparisons++;
                if (array[c] == element) // сравниваем центральный элемент с искомымы, если нашли, то возвращаем ответ true и индекс элемента
                {
                    index = c;
                    return true;
                }
                if (element < array[c]) // если мы не нашли элемент, то сдвигаем границу поиска, ориентируясь на отношение между центральным и искомым элементом (в данном случае элемент меньше центрального, то сдвигаем правую границу, в ином случае левую границу)
                {
                    r = c - 1;
                }
                else
                {
                    l = c + 1; //сдвигаем леву границу
                }
            }
            index = -1;
            return false; // если так и не нашли элемент - возвращаем false и индекс -1
        }
        static void Main(string[] args)
        {
            int index;
            int comparisons;
            //Utilities.Generate();
            List<List<int>> l = ReadFromFile(new StreamReader("in.txt")); // читаем данные с входного файла
            if (l.Count == 0) // проверка на пустоту файла
            { 
                Console.WriteLine("нет массива"); 
                Console.ReadKey(); 
                return;
            }
            Console.WriteLine("Введите элемент для поиска");
            int element = Convert.ToInt32(Console.ReadLine()); // ввдоим элемент для поиска
            SequentialSearch(new List<int>(1), 1, out index, out comparisons); // вызываем функции для предворительной компиляции, для более точных значений позднее
            BinarySearch(new List<int>(1), 1, out index, out comparisons);
            int[,] m = new int[l.Count,4]; // массив, в котором будем хранить результаты тестов
            Stopwatch sw = new Stopwatch(); // Счетчик прошедшего времени
            float[,] time = new float[l.Count,2];
            for (int i = 0; i < l.Count; i++)
            {
                List<int> arr = l[i]; // построчно считываем последовательности из массива
                sw.Start(); // начинаем замер времени
                SequentialSearch(arr, element, out index, out comparisons); // вызов алгоритма поиска
                sw.Stop(); // останавливаем счетчик
                m[i, 0] = comparisons; // записываем кол-во сравнений
                m[i, 2] = index; // записываем индекс
                m[i, 3] = arr.Count; // записываем длину массива
                time[i, 0] = (float)sw.Elapsed.Ticks/TimeSpan.TicksPerMillisecond; // записываем время
                sw.Reset(); // сбрасываем счетчик
                sw.Start(); // начинаем замер времени
                BinarySearch(arr, element, out index, out comparisons); // вызов алгоритма поиска
                sw.Stop(); // останавливаем счетчик
                time[i, 1] = (float)sw.Elapsed.Ticks / TimeSpan.TicksPerMillisecond; // записываем время
                sw.Reset(); // сбрасываем счетчик
                m[i, 1] = comparisons; // записываем кол-во сравнений
            }
            PrintLine(); // отрисовываем верхнюю границу таблицы
            PrintRow(true,"Алгоритм","Длина массива","Кол-во итераций","Найден ли элемент", "Время (мс)"); // рисуем названия столбцов
            for (int i = 0; i < m.GetLength(0); i++) // перебираем полученные данные
            {
                PrintRow(false,"Линейный", m[i, 3].ToString(), m[i, 0].ToString(), m[i, 2] != -1 ? "Yes" : "No", time[i, 0].ToString("0.00000")); // выводим данные для алгоритма линейного поиска при обработке i-го массива 
                PrintRow(false,"Бинарный", m[i, 3].ToString(), m[i, 1].ToString(), m[i, 2] != -1 ? "Yes" : "No", time[i, 1].ToString("0.00000")); // выводим данные для алгоритма бинарного поиска при обработке i-го массива 
            }
            PrintLine(); // отрисовываем нижнюю границу таблицы
            Console.ReadKey();
        }
    }
}