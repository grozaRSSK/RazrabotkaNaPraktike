using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LibDeist
{
    public class Lib_8
    {
        /// <summary>
        /// Проверка, является ли массив геометрической прогрессией.
        /// </summary>
        /// <param name="mas">Проверяемый массив</param>
        /// <param name="q">Знаменатель прогрессии (если true)</param>
        /// <returns>true — является геометрической прогрессией</returns>
        public static bool Сalcul(int[] mas, out double q)
        {
            q = 0;

            if (mas == null || mas.Length < 3)
                return false;

            if (mas[0] == 0)
                return false;

            q = (double)mas[1] / mas[0];

            if (q == 0)
                return false;

            for (int i = 1; i < mas.Length; i++)
            {
                if (mas[i - 1] == 0)
                    return false;

                double currentQ = (double)mas[i] / mas[i - 1];

                if (Math.Abs(currentQ - q) > 0.001)
                    return false;
            }
            return true;
        }
    }
}