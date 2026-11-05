using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Variant_8
{
    static class LibMas
    {
        /// <summary>
        /// Метод для одномерного массива ввод
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this T[] arr)
        {
            var res = new DataTable();
            for (int i = 0; i < arr.Length; i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            var row = res.NewRow();
            for (int i = 0; i < arr.Length; i++)
            {
                row[i] = arr[i];
            }
            res.Rows.Add(row);
            return res;
        }


        /// <summary>
        /// Заполнение рандомными значениями
        /// </summary>
        /// <param name="count"></param>
        /// <param name="otr"></param>
        /// <param name="pol"></param>
        /// <returns></returns>
        public static int[] Random(int count, bool otr = false, bool pol = false)
        {
            int[] mas = new int[count];

            Random rnd = new Random();
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] = rnd.Next(-100, 100);
            }
            return mas;
        }
    }
}