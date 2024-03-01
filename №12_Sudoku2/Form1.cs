using System;
using System.Drawing;
using System.Windows.Forms;

namespace _12_Sudoku2
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView;
        private Color[,] blockColors = {
            { Color.LightBlue, Color.LightCoral, Color.LightGreen },
            { Color.LightGoldenrodYellow, Color.LightPink, Color.LightSalmon },
            { Color.LightSkyBlue, Color.LightGray, Color.LightSeaGreen }
        };

        public Form1()
        {
            InitializeComponent();
            InitializeGridView();
            ColorAllBlocks();

            Button btnGenerate = new Button();
            btnGenerate.Text = "������������� ������";
            btnGenerate.Size = new Size(150, 30);
            btnGenerate.Location = new Point(10, 300);

            btnGenerate.Click += new EventHandler(btnGenerateSudoku_Click);

            this.Controls.Add(btnGenerate);

            Button btnClear = new Button();
            btnClear.Text = "�������� ����";
            btnClear.Size = new Size(150, 30);
            btnClear.Location = new Point(180, 300);

            btnClear.Click += new EventHandler(btnClearFields_Click);

            this.Controls.Add(btnClear);
        }

        // ������������� GridView
        private void InitializeGridView()
        {
            dataGridView = new DataGridView();
            dataGridView.Size = new Size(270, 270);
            dataGridView.Location = new Point(10, 10);
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView.MultiSelect = false;

            for (int i = 0; i < 9; i++)
            {
                dataGridView.Columns.Add("col" + i, "");
                dataGridView.Columns[i].Width = 30;
                dataGridView.Columns[i].DefaultCellStyle.Font = new Font(dataGridView.Font.FontFamily, 16);
            }

            for (int i = 0; i < 9; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Height = 30;
            }

            this.Controls.Add(dataGridView);
        }

        // ����� ��� ��������� ����� ������ ����� 3x3
        private void ColorBlock(int blockRow, int blockCol)
        {
            for (int i = blockRow * 3; i < (blockRow + 1) * 3; i++)
            {
                for (int j = blockCol * 3; j < (blockCol + 1) * 3; j++)
                {
                    dataGridView[j, i].Style.BackColor = blockColors[blockRow, blockCol];
                    dataGridView[j, i].Style.SelectionBackColor = blockColors[blockRow, blockCol];
                    dataGridView[j, i].Style.Font = new Font(dataGridView.Font.FontFamily, 16);
                }
            }
        }

        // ����� ��� ���������� ���� ������
        private void ColorAllBlocks()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ColorBlock(i, j);
                }
            }
        }

        // ������������� ������
        private void GenerateSudoku()
        {
            int[,] sudoku = new int[9, 9];
            GenerateSudokuRecursively(sudoku);
            DisplaySudoku(sudoku);
        }

        // ����������� ����� ��� ��������� ������
        private bool GenerateSudokuRecursively(int[,] sudoku)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (sudoku[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsSafe(sudoku, row, col, num))
                            {
                                sudoku[row, col] = num;
                                if (GenerateSudokuRecursively(sudoku))
                                    return true;
                                sudoku[row, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        // ��������, ��������� �� ���������� ����� � ������ ������
        private bool IsSafe(int[,] sudoku, int row, int col, int num)
        {
            return !UsedInRow(sudoku, row, num) && !UsedInCol(sudoku, col, num) && !UsedInBox(sudoku, row - row % 3, col - col % 3, num);
        }

        // ��������, �������������� �� ����� � ������ ������
        private bool UsedInRow(int[,] sudoku, int row, int num)
        {
            for (int col = 0; col < 9; col++)
            {
                if (sudoku[row, col] == num)
                    return true;
            }
            return false;
        }

        // ��������, �������������� �� ����� � ������ �������
        private bool UsedInCol(int[,] sudoku, int col, int num)
        {
            for (int row = 0; row < 9; row++)
            {
                if (sudoku[row, col] == num)
                    return true;
            }
            return false;
        }

        // ��������, �������������� �� ����� � ������ ����� 3x3
        private bool UsedInBox(int[,] sudoku, int boxStartRow, int boxStartCol, int num)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (sudoku[row + boxStartRow, col + boxStartCol] == num)
                        return true;
                }
            }
            return false;
        }

        // ����������� ���������������� ������
        private void DisplaySudoku(int[,] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    dataGridView[j, i].Value = sudoku[i, j].ToString();
                }
            }
        }


        // ���������� ������� ������ "������������� ������"
        private void btnGenerateSudoku_Click(object sender, EventArgs e)
        {
            GenerateSudoku();
        }

        // ����� ������� �����
        private void ClearAllFields()
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = null;
                }
            }
        }

        // ���������� ������� ��� ������� ������ "��������"
        private void btnClearFields_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
    }
}
