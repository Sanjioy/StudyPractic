using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _16_Points
{
    public partial class Form1 : Form
    {
        // Список для хранения точек.
        private List<PointF> points = new List<PointF>();

        // Радиус точек.
        private const float pointRadius = 5;

        // Расстояние для создания и удаления связей между точками.
        private const float connectionDistance = 50;

        // Размеры поля.
        private float fieldWidth = 800;
        private float fieldHeight = 600;

        // Скорость движения точек.
        private const float pointSpeed = 2;

        // Графический объект для рисования на форме.
        private Graphics graphics;

        // Конструктор формы.
        public Form1()
        {
            // Инициализация формы.
            InitializeForm();

            // Запуск генерации и движения точек.
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 30; // Интервал обновления в миллисекундах.
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Метод для инициализации формы.
        private void InitializeForm()
        {
            // Установка размеров формы.
            this.Size = new Size((int)fieldWidth, (int)fieldHeight);

            // Инициализация графики.
            graphics = this.CreateGraphics();

            // Обработка события отрисовки формы.
            this.Paint += MainForm_Paint;
        }

        // Обработчик таймера для движения точек.
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Генерация и движение точек.
            GenerateAndMovePoints();

            // Перерисовка формы.
            this.Invalidate();
        }

        // Метод для генерации и движения точек.
        private void GenerateAndMovePoints()
        {
            // Генерация новой точки.
            PointF newPoint = new PointF((float)new Random().NextDouble() * fieldWidth, (float)new Random().NextDouble() * fieldHeight);

            // Создайте копию списка точек.
            List<PointF> pointsCopy = new List<PointF>(points);

            // Движение точек.
            foreach (PointF point in pointsCopy)
            {
                // Вычисление угла между текущей точкой и новой точкой.
                float angle = (float)Math.Atan2(newPoint.Y - point.Y, newPoint.X - point.X);

                // Вычисление новых координат точки с учетом выбранного направления.
                float deltaX = (float)(pointSpeed * Math.Cos(angle));
                float deltaY = (float)(pointSpeed * Math.Sin(angle));

                // Обновление координат точек.
                PointF updatedPoint = new PointF(point.X + deltaX, point.Y + deltaY);

                // Проверка столкновения с границами поля и обработка отражения.
                CheckAndHandleBoundaryCollision(ref updatedPoint, deltaX, deltaY);

                // Обновление координат точек в списке.
                int index = points.IndexOf(point);
                points[index] = updatedPoint;

                // Проверка создания и удаления связей между точками.
                CheckAndHandleConnections(point, newPoint);
            }

            // Добавление новой точки в список.
            points.Add(newPoint);
        }

        // Метод для проверки столкновения с границами поля и обработки отражения.
        private void CheckAndHandleBoundaryCollision(ref PointF point, float deltaX, float deltaY)
        {
            if (point.X - pointRadius < 0 || point.X + pointRadius > fieldWidth)
            {
                deltaX = -deltaX; // Отражение по горизонтали.
            }

            if (point.Y - pointRadius < 0 || point.Y + pointRadius > fieldHeight)
            {
                deltaY = -deltaY; // Отражение по вертикали.
            }

            point = new PointF(point.X + deltaX, point.Y + deltaY);
        }

        // Метод для проверки создания и удаления связей между точками.
        private void CheckAndHandleConnections(PointF point1, PointF point2)
        {
            foreach (PointF p in points)
            {
                if (p != point1 && Distance(point1, p) <= connectionDistance)
                {
                    // Создание связи.
                    DrawConnection(point1, p);
                }
                else if (p != point1 && Distance(point1, p) > connectionDistance)
                {
                    // Удаление связи.
                    RemoveConnection(point1, p);
                }

                if (p != point2 && Distance(point2, p) <= connectionDistance)
                {
                    // Создание связи.
                    DrawConnection(point2, p);
                }
                else if (p != point2 && Distance(point2, p) > connectionDistance)
                {
                    // Удаление связи.
                    RemoveConnection(point2, p);
                }
            }
        }

        // Метод для отрисовки связи между точками.
        private void DrawConnection(PointF point1, PointF point2)
        {
            graphics.DrawLine(Pens.Black, point1, point2);
        }

        // Метод для удаления связи между точками.
        private void RemoveConnection(PointF point1, PointF point2)
        {
            // Реализация удаления связи (если необходимо).
        }

        // Метод для вычисления расстояния между двумя точками.
        private float Distance(PointF point1, PointF point2)
        {
            return (float)Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        // Обработчик события отрисовки формы.
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // Отрисовка точек.
            foreach (PointF point in points)
            {
                graphics.FillEllipse(Brushes.Red, point.X - pointRadius, point.Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
            }
        }
    }
}
