using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _14_Infection
{
    public partial class Form1 : Form
    {
        // Размер квадрата
        private int N;

        // Интервал времени (в миллисекундах)
        private int timeInterval;

        // Состояния клеток (0 - здоровая, 1 - зараженная, 2 - невосприимчивая)
        private int[,] cellStates;

        // Графический объект для рисования
        private Graphics graphics;

        // Кисти для закрашивания клеток разными цветами
        private SolidBrush healthyBrush = new SolidBrush(Color.Green);
        private SolidBrush infectedBrush = new SolidBrush(Color.Red);
        private SolidBrush immuneBrush = new SolidBrush(Color.Blue);

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeSimulation()
        {
            // Инициализация переменных и состояний клеток
            N = int.Parse(textBoxN.Text);
            timeInterval = int.Parse(textBoxTimeInterval.Text);
            cellStates = new int[N, N];
            cellStates[N / 2, N / 2] = 1; // Исходная зараженная клетка

            // Создание графического объекта
            graphics = panelSimulation.CreateGraphics();

            // Запуск моделирования в отдельном потоке
            Thread simulationThread = new Thread(new ThreadStart(SimulateInfection));
            simulationThread.Start();
        }

        private void SimulateInfection()
        {
            while (true)
            {
                // Моделирование распространения инфекции
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (cellStates[i, j] == 1)
                        {
                            // Клетка заражена, попытка заразить соседние клетки
                            TryInfectNeighbors(i, j);
                        }
                        else if (cellStates[i, j] == 2)
                        {
                            // Клетка невосприимчива, уменьшение времени иммунитета
                            cellStates[i, j]--;
                        }
                    }
                }

                // Проверка на закрытие формы перед обновлением
                if (!IsDisposed)
                {
                    // Обновление состояния клеток для отображения
                    Invoke(new MethodInvoker(UpdateCellColors));
                }

                // Пауза перед следующим интервалом
                Thread.Sleep(timeInterval);
            }
        }

        private void TryInfectNeighbors(int x, int y)
        {
            // Вероятность заражения соседней клетки
            double infectionProbability = 0.5;

            // Заражение соседних клеток
            for (int i = Math.Max(0, x - 1); i <= Math.Min(N - 1, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(N - 1, y + 1); j++)
                {
                    if (i != x || j != y)
                    {
                        if (cellStates[i, j] == 0 && (new Random().NextDouble() < infectionProbability))
                        {
                            cellStates[i, j] = 1; // Заражение соседней клетки
                        }
                    }
                }
            }

            // После шести единиц времени становится невосприимчивой
            if (cellStates[x, y] == 1)
            {
                cellStates[x, y]++;
            }
        }

        private void UpdateCellColors()
        {
            // Отображение текущего состояния клеток на форме
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
            // Запуск симуляции при нажатии кнопки "Старт"
            InitializeSimulation();
        }
    }
}
