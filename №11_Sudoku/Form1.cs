using System;
using System.Drawing;
using System.Windows.Forms;

namespace _11_Sudoku
{
    public partial class Form1 : Form
    {
        private int[,] sudokuGrid = new int[9, 9];

        public Form1()
        {
            InitializeComponent();
            SolveSudoku();
            DisplaySudoku();
        }

        private bool IsSafe(int row, int col, int num)
        {
            // Проверка, безопасно ли вставить число num в указанную ячейку
            for (int x = 0; x < 9; x++)
            {
                if (sudokuGrid[row, x] == num || sudokuGrid[x, col] == num || sudokuGrid[row - row % 3 + x / 3, col - col % 3 + x % 3] == num)
                {
                    return false;
                }
            }
            return true;
        }

        private bool SolveSudokuUtil()
        {
            // Пошаговое решение судоку
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (sudokuGrid[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsSafe(row, col, num))
                            {
                                sudokuGrid[row, col] = num;

                                if (SolveSudokuUtil())
                                {
                                    return true;
                                }

                                sudokuGrid[row, col] = 0;
                            }
                        }

                        return false;
                    }
                }
            }

            return true;
        }

        private void SolveSudoku()
        {
            if (!SolveSudokuUtil())
            {
                MessageBox.Show("Нет решения для данного судоку.");
            }
        }

        private void DisplaySudoku()
        {
            DataGridView sudokuGridView = new DataGridView
            {
                Parent = this,
                Size = new System.Drawing.Size(300, 300),
                Location = new System.Drawing.Point(50, 50),
                RowCount = 9,
                ColumnCount = 9,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudokuGridView[j, i].Value = sudokuGrid[i, j];
                }
            }

            // Добавим кнопку для вывода судоку в файл или на печать
            Button outputButton = new Button
            {
                Text = "Вывести",
                Location = new System.Drawing.Point(400, 50),
                Size = new System.Drawing.Size(80, 30)
            };
            outputButton.Click += OutputButton_Click;
            this.Controls.Add(outputButton);
        }

        private void OutputButton_Click(object sender, EventArgs e)
        {
            // При нажатии кнопки выводим судоку в файл или на печать
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Сохранить судоку"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                writer.Write(sudokuGrid[i, j] + " ");
                            }
                            writer.WriteLine();
                        }
                    }

                    MessageBox.Show("Судоку успешно сохранено в файл: " + filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении судоку в файл: " + ex.Message);
                }
            }
        }
    }
}
