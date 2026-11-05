using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RazrabotkaNaPraktike
{
    public class ChartDrawer
    {

        /// <summary>
        /// Строит график по массиву данных.
        /// </summary>
        /// <param name="canvas">Контейнер для рисования</param>
        /// <param name="data">Массив значений</param>
        public static void Draw(Canvas canvas, int[] data)
        {
            if (canvas != null && data != null && data.Length > 0)
            {
                canvas.Children.Clear();

                double width = 680;
                double height = 600;

                double margin = 50; 

                double drawW = width - 2 * margin;
                double drawH = height - 2 * margin;

                int min = data[0];
                int max = data[0];

                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] < min) min = data[i];
                    if (data[i] > max) max = data[i];
                }

                if (max == min)
                {
                    min = min - 1;
                    max = max + 1;
                }


                double stepX = drawW;
                if (data.Length > 1) stepX = drawW / (data.Length - 1);

                Brush blakBrush = Brushes.Black;
                double axisYbottom = margin + drawH;
                double axisXright = margin + drawW;

                Line axisY = new Line();
                axisY.X1 = margin;
                axisY.Y1 = margin - 20;
                axisY.X2 = margin;
                axisY.Y2 = axisYbottom;
                axisY.Stroke = blakBrush;
                axisY.StrokeThickness = 2;
                canvas.Children.Add(axisY);

                Line axisX = new Line();
                axisX.X1 = margin;
                axisX.Y1 = axisYbottom;
                axisX.X2 = axisXright + 20;
                axisX.Y2 = axisYbottom;
                axisX.Stroke = blakBrush;
                axisX.StrokeThickness = 2;
                canvas.Children.Add(axisX);

                Polyline arrowY = new Polyline();
                arrowY.Points = new PointCollection();
                arrowY.Points.Add(new Point(margin - 5, margin - 10));
                arrowY.Points.Add(new Point(margin, margin - 20));
                arrowY.Points.Add(new Point(margin + 5, margin - 10));
                arrowY.Stroke = blakBrush;
                arrowY.StrokeThickness = 2;
                canvas.Children.Add(arrowY);

                Polyline arrowX = new Polyline();
                arrowX.Points = new PointCollection();
                arrowX.Points.Add(new Point(axisXright + 10, axisYbottom - 5));
                arrowX.Points.Add(new Point(axisXright + 20, axisYbottom));
                arrowX.Points.Add(new Point(axisXright + 10, axisYbottom + 5));
                arrowX.Stroke = blakBrush;
                arrowX.StrokeThickness = 2;
                canvas.Children.Add(arrowX);

                TextBlock labelY = new TextBlock();
                labelY.Text = "Y";
                labelY.Foreground = blakBrush;
                labelY.FontWeight = FontWeights.Bold;
                labelY.FontSize = 16;
                Canvas.SetLeft(labelY, margin - 25);
                Canvas.SetTop(labelY, margin - 25);
                canvas.Children.Add(labelY);

                TextBlock labelX = new TextBlock();
                labelX.Text = "X";
                labelX.Foreground = blakBrush;
                labelX.FontWeight = FontWeights.Bold;
                labelX.FontSize = 16;
                Canvas.SetLeft(labelX, axisXright + 10);
                Canvas.SetTop(labelX, axisYbottom + 10);
                canvas.Children.Add(labelX);

                for (int i = 0; i < data.Length; i++)
                {
                    double x = margin + i * stepX;

                    Line tickX = new Line();
                    tickX.X1 = x;
                    tickX.Y1 = axisYbottom;
                    tickX.X2 = x;
                    tickX.Y2 = axisYbottom + 6;
                    tickX.Stroke = blakBrush;
                    tickX.StrokeThickness = 2;
                    canvas.Children.Add(tickX);

                    TextBlock tb = new TextBlock();
                    tb.Text = (i + 1).ToString();
                    tb.Foreground = blakBrush;
                    tb.FontSize = 11;
                    tb.Measure(new Size(double.PositiveInfinity,
                        double.PositiveInfinity));
                    Canvas.SetLeft(tb, x - tb.DesiredSize.Width / 2);
                    Canvas.SetTop(tb, axisYbottom + 8);
                    canvas.Children.Add(tb);
                }

                int yTicks = 5;
                for (int i = 0; i <= yTicks; i++)
                {
                    double val = min + (max - min) * i / yTicks;
                    double y = margin + drawH - (val - min) * drawH / (max - min);

                    Line tickY = new Line();
                    tickY.X1 = margin - 6;
                    tickY.Y1 = y;
                    tickY.X2 = margin;
                    tickY.Y2 = y;
                    tickY.Stroke = blakBrush;
                    tickY.StrokeThickness = 2;
                    canvas.Children.Add(tickY);

                    TextBlock tb = new TextBlock();
                    tb.Text = Math.Round(val).ToString(); // Округляем для красоты
                    tb.Foreground = blakBrush;
                    tb.FontSize = 11;
                    tb.Measure(new Size(double.PositiveInfinity,
                        double.PositiveInfinity));
                    Canvas.SetLeft(tb, margin - 8 - tb.DesiredSize.Width);
                    Canvas.SetTop(tb, y - 7);
                    canvas.Children.Add(tb);
                }

                Polyline polyline = new Polyline();
                polyline.Stroke = Brushes.Gray;
                polyline.StrokeThickness = 2;
                polyline.StrokeLineJoin = PenLineJoin.Round;

                for (int i = 0; i < data.Length; i++)
                {
                    double x = margin + i * stepX;
                    double y = margin + drawH - (data[i] - min) * drawH / (max - min);
                    polyline.Points.Add(new Point(x, y));
                }
                canvas.Children.Add(polyline);

                for (int i = 0; i < data.Length; i++)
                {
                    double x = margin + i * stepX;
                    double y = margin + drawH - (data[i] - min) * drawH / (max - min);

                    Ellipse point = new Ellipse();
                    point.Width = 6;
                    point.Height = 6;
                    point.Fill = Brushes.CornflowerBlue;
                    point.Stroke = Brushes.CornflowerBlue;
                    point.StrokeThickness = 1;

                    Canvas.SetLeft(point, x - 3);
                    Canvas.SetTop(point, y - 3);
                    canvas.Children.Add(point);
                }
            }
        }
    }
}
