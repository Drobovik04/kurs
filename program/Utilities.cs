using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program
{
    class Utilities
    {
        static int tableWidth = 90;
        static StreamWriter writer = new StreamWriter(new FileStream("out.txt", FileMode.Create));
        public static void Generate()
        {
            File.WriteAllText("in.txt", string.Empty);
            StreamWriter strw = new StreamWriter("in.txt");
            Random r = new Random();
            int[] l = new int[10];
            for (int i = 0; i < 10; i++)
            {
                l[i] = r.Next(10000);
            }
            Array.Sort(l);
            strw.WriteLine(string.Join(" ", l.Select(x => x.ToString())));
            l = new int[100];
            for (int i = 0; i < 100; i++)
            {
                l[i] = r.Next(10000);
            }
            Array.Sort(l);
            strw.WriteLine(string.Join(" ", l.Select(x => x.ToString())));
            l = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                l[i] = r.Next(10000);
            }
            Array.Sort(l);
            strw.WriteLine(string.Join(" ", l.Select(x => x.ToString())));
            l = new int[10000];
            for (int i = 0; i < 10000; i++)
            {
                l[i] = r.Next(10000);
            }
            Array.Sort(l);
            strw.WriteLine(string.Join(" ", l.Select(x => x.ToString())));
            l = new int[100000];
            for (int i = 0; i < 100000; i++)
            {
                l[i] = r.Next(100000);
            }
            Array.Sort(l);
            strw.WriteLine(string.Join(" ", l.Select(x => x.ToString())));
            strw.Flush();
            strw.Close();
        }
        public static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
            writer.WriteLine(new string('-', tableWidth));
            writer.Flush();
        }

        public static void PrintRow(bool align, params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";
            if (align)
            {
                foreach (string column in columns)
                {
                    row += AlignCentre(column, width) + "|";
                }
            }
            else
            {
                foreach (string column in columns)
                {
                    row += AlignLeft(column, width) + "|";
                }
            }

            Console.WriteLine(row);
            writer.WriteLine(row);
            writer.Flush();
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        static string AlignLeft(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text+new string(' ',width-text.Length);
            }
        }
    }
}
