using LibDeist;
using System.Windows;
using System.Windows.Controls;
using Variant_8;

namespace RazrabotkaNaPraktike
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] mas;

        public MainWindow()
        {
            InitializeComponent();
            Ctolb.Focus();
        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(Ctolb.Text, out int count) && count > 0)
            {
                mas = LibMas.Random(count);

                NumbersDataGrid.ItemsSource = LibMas.ToDataTable(mas).DefaultView;

                Rezult.Clear();

                ChartDrawer.Draw(ChartCanvas, mas);
            }
            else
            {
                MessageBox.Show("Неверные данные!", "Ошибка");
            }
            Ctolb.Focus();
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null || mas.Length == 0)
            {
                MessageBox.Show("Вы не создали массив, укажите количество элементов и нажмите кнопку ");
                return;
            }

            bool isGeo = Lib_8.Сalcul(mas, out double q);

            if (isGeo)
                Rezult.Text = "ДА. Последовательность является геометрической прогрессией.\nЗнаменатель q = "
                    + Math.Round(q, 2).ToString();
            else
                Rezult.Text = "НЕТ. Последовательность не является геометрической прогрессией.";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NumbersDataGrid.ItemsSource = null;
            mas = null;
            Ctolb.Clear();
            Rezult.Clear();
            if (ChartCanvas != null)
                ChartCanvas.Children.Clear();
            Ctolb.Focus();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show( "ПРОГРАММА: Построение графика последовательности\n" +
            "Разработчик: студент группы ИСП-41 Громов Андрей\n" +
            "Практическая работа, вариант №8\n\n" +
            "ЗАДАНИЕ:\n" +
            "Определить, является ли введенная последовательность чисел геометрической прогрессией. " +
            "Построить график изменения последовательности (график строится по мере ввода данных).\n\n" +
            "КАК РАССЧИТЫВАЕТСЯ:\n" +
            "1. Массив проверяется на длину (минимум 3 элемента).\n" +
            "2. Вычисляется знаменатель прогрессии q = mas[1] / mas[0].\n" +
            "3. Для каждой пары соседних элементов проверяется условие: mas[i] / mas[i-1] == q\n" +
            "   (с учётом погрешности: разница не должна превышать 0.001).\n" +
            "4. Если все пары подходят под условие — последовательность является геометрической прогрессией.\n\n" +
            "ГРАФИК:\n" +
            "Строится на объекте Canvas. Ось X — порядковый номер элемента, ось Y — значение элемента. " +
            "Точки соединяются ломаной линией. При редактировании таблицы график перестраивается автоматически.",
            "Информация о программе",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumbersDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexColumn = e.Column.DisplayIndex;
            var textBox = (TextBox)e.EditingElement;

            if (Int32.TryParse(textBox.Text, out int newValue))
            {
                mas[indexColumn] = newValue;
                ChartDrawer.Draw(ChartCanvas, mas);
            }
            else
            {
                MessageBox.Show("Введите целое число!", "Ошибка");
                textBox.Text = mas[indexColumn].ToString();
            }
        }
    }
}