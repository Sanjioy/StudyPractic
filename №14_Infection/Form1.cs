using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _14_Infection
{
    public partial class Form1 : Form
    {
        // ������ ��������
        private int N;

        // �������� ������� (� �������������)
        private int timeInterval;

        // ��������� ������ (0 - ��������, 1 - ����������, 2 - ���������������)
        private int[,] cellStates;

        // ����������� ������ ��� ���������
        private Graphics graphics;

        // ����� ��� ������������ ������ ������� �������
        private SolidBrush healthyBrush = new SolidBrush(Color.Green);
        private SolidBrush infectedBrush = new SolidBrush(Color.Red);
        private SolidBrush immuneBrush = new SolidBrush(Color.Blue);

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeSimulation()
        {
            // ������������� ���������� � ��������� ������
            N = int.Parse(textBoxN.Text);
            timeInterval = int.Parse(textBoxTimeInterval.Text);
            cellStates = new int[N, N];
            cellStates[N / 2, N / 2] = 1; // �������� ���������� ������

            // �������� ������������ �������
            graphics = panelSimulation.CreateGraphics();

            // ������ ������������� � ��������� ������
            Thread simulationThread = new Thread(new ThreadStart(SimulateInfection));
            simulationThread.Start();
        }

        private void SimulateInfection()
        {
            while (true)
            {
                // ������������� ��������������� ��������
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (cellStates[i, j] == 1)
                        {
                            // ������ ��������, ������� �������� �������� ������
                            TryInfectNeighbors(i, j);
                        }
                        else if (cellStates[i, j] == 2)
                        {
                            // ������ ��������������, ���������� ������� ����������
                            cellStates[i, j]--;
                        }
                    }
                }

                // �������� �� �������� ����� ����� �����������
                if (!IsDisposed)
                {
                    // ���������� ��������� ������ ��� �����������
                    Invoke(new MethodInvoker(UpdateCellColors));
                }

                // ����� ����� ��������� ����������
                Thread.Sleep(timeInterval);
            }
        }

        private void TryInfectNeighbors(int x, int y)
        {
            // ����������� ��������� �������� ������
            double infectionProbability = 0.5;

            // ��������� �������� ������
            for (int i = Math.Max(0, x - 1); i <= Math.Min(N - 1, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(N - 1, y + 1); j++)
                {
                    if (i != x || j != y)
                    {
                        if (cellStates[i, j] == 0 && (new Random().NextDouble() < infectionProbability))
                        {
                            cellStates[i, j] = 1; // ��������� �������� ������
                        }
                    }
                }
            }

            // ����� ����� ������ ������� ���������� ���������������
            if (cellStates[x, y] == 1)
            {
                cellStates[x, y]++;
            }
        }

        private void UpdateCellColors()
        {
            // ����������� �������� ��������� ������ �� �����
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (cellStates[i, j] == 0)
                    {
                        graphics.FillRectangle(healthyBrush, i * 20, j * 20, 20, 20);
                    }
                    else if (cellStates[i, j] == 1)
                    {
                        graphics.FillRectangle(infectedBrush, i * 20, j * 20, 20, 20);
                    }
                    else if (cellStates[i, j] == 2)
                    {
                        graphics.FillRectangle(immuneBrush, i * 20, j * 20, 20, 20);
                    }
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // ������ ��������� ��� ������� ������ "�����"
            InitializeSimulation();
        }
    }
}
