using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _16_Points
{
    public partial class Form1 : Form
    {
        // ������ ��� �������� �����.
        private List<PointF> points = new List<PointF>();

        // ������ �����.
        private const float pointRadius = 5;

        // ���������� ��� �������� � �������� ������ ����� �������.
        private const float connectionDistance = 50;

        // ������� ����.
        private float fieldWidth = 800;
        private float fieldHeight = 600;

        // �������� �������� �����.
        private const float pointSpeed = 2;

        // ����������� ������ ��� ��������� �� �����.
        private Graphics graphics;

        // ����������� �����.
        public Form1()
        {
            // ������������� �����.
            InitializeForm();

            // ������ ��������� � �������� �����.
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 30; // �������� ���������� � �������������.
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // ����� ��� ������������� �����.
        private void InitializeForm()
        {
            // ��������� �������� �����.
            this.Size = new Size((int)fieldWidth, (int)fieldHeight);

            // ������������� �������.
            graphics = this.CreateGraphics();

            // ��������� ������� ��������� �����.
            this.Paint += MainForm_Paint;
        }

        // ���������� ������� ��� �������� �����.
        private void Timer_Tick(object sender, EventArgs e)
        {
            // ��������� � �������� �����.
            GenerateAndMovePoints();

            // ����������� �����.
            this.Invalidate();
        }

        // ����� ��� ��������� � �������� �����.
        private void GenerateAndMovePoints()
        {
            // ��������� ����� �����.
            PointF newPoint = new PointF((float)new Random().NextDouble() * fieldWidth, (float)new Random().NextDouble() * fieldHeight);

            // �������� ����� ������ �����.
            List<PointF> pointsCopy = new List<PointF>(points);

            // �������� �����.
            foreach (PointF point in pointsCopy)
            {
                // ���������� ���� ����� ������� ������ � ����� ������.
                float angle = (float)Math.Atan2(newPoint.Y - point.Y, newPoint.X - point.X);

                // ���������� ����� ��������� ����� � ������ ���������� �����������.
                float deltaX = (float)(pointSpeed * Math.Cos(angle));
                float deltaY = (float)(pointSpeed * Math.Sin(angle));

                // ���������� ��������� �����.
                PointF updatedPoint = new PointF(point.X + deltaX, point.Y + deltaY);

                // �������� ������������ � ��������� ���� � ��������� ���������.
                CheckAndHandleBoundaryCollision(ref updatedPoint, deltaX, deltaY);

                // ���������� ��������� ����� � ������.
                int index = points.IndexOf(point);
                points[index] = updatedPoint;

                // �������� �������� � �������� ������ ����� �������.
                CheckAndHandleConnections(point, newPoint);
            }

            // ���������� ����� ����� � ������.
            points.Add(newPoint);
        }

        // ����� ��� �������� ������������ � ��������� ���� � ��������� ���������.
        private void CheckAndHandleBoundaryCollision(ref PointF point, float deltaX, float deltaY)
        {
            if (point.X - pointRadius < 0 || point.X + pointRadius > fieldWidth)
            {
                deltaX = -deltaX; // ��������� �� �����������.
            }

            if (point.Y - pointRadius < 0 || point.Y + pointRadius > fieldHeight)
            {
                deltaY = -deltaY; // ��������� �� ���������.
            }

            point = new PointF(point.X + deltaX, point.Y + deltaY);
        }

        // ����� ��� �������� �������� � �������� ������ ����� �������.
        private void CheckAndHandleConnections(PointF point1, PointF point2)
        {
            foreach (PointF p in points)
            {
                if (p != point1 && Distance(point1, p) <= connectionDistance)
                {
                    // �������� �����.
                    DrawConnection(point1, p);
                }
                else if (p != point1 && Distance(point1, p) > connectionDistance)
                {
                    // �������� �����.
                    RemoveConnection(point1, p);
                }

                if (p != point2 && Distance(point2, p) <= connectionDistance)
                {
                    // �������� �����.
                    DrawConnection(point2, p);
                }
                else if (p != point2 && Distance(point2, p) > connectionDistance)
                {
                    // �������� �����.
                    RemoveConnection(point2, p);
                }
            }
        }

        // ����� ��� ��������� ����� ����� �������.
        private void DrawConnection(PointF point1, PointF point2)
        {
            graphics.DrawLine(Pens.Black, point1, point2);
        }

        // ����� ��� �������� ����� ����� �������.
        private void RemoveConnection(PointF point1, PointF point2)
        {
            // ���������� �������� ����� (���� ����������).
        }

        // ����� ��� ���������� ���������� ����� ����� �������.
        private float Distance(PointF point1, PointF point2)
        {
            return (float)Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        // ���������� ������� ��������� �����.
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // ��������� �����.
            foreach (PointF point in points)
            {
                graphics.FillEllipse(Brushes.Red, point.X - pointRadius, point.Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
            }
        }
    }
}
