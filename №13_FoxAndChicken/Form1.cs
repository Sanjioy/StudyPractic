using System;
using System.Threading;
using System.Windows.Forms;

namespace _13_FoxAndChicken
{
    public partial class Form1 : Form
    {
        // Инициализация кнопок для лис
        private Button foxField1;
        private Button foxField2;

        // Счетчик пойманных куриц
        private int eatenChicks = 0;

        // Двумерный массив кнопок для игрового поля
        private readonly Button[,] gameButtons;

        // Кнопки для начала и конца хода
        private Button startButton = new Button { Text = "" };
        private Button endButton = new Button { Text = "" };

        // Координаты лис на игровом поле
        private int foxField1Row = 2;
        private int foxField1Column = 2;
        private int foxField2Row = 2;
        private int foxField2Column = 4;

        // Генератор случайных чисел для движения лис
        private readonly Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            // Присвоение кнопок лис
            foxField1 = (Button)Controls["button9"];
            foxField2 = (Button)Controls["button11"];

            // Инициализация игрового поля
            gameButtons = new Button[7, 7]
            {
                { null, null, button1, button2, button3, null, null },
                { null, null, button4, button5, button6, null, null },
                { button7, button8, button9, button10, button11, button12, button13 },
                { button14, button15, button16, button17, button18, button19, button20 },
                { button21, button22, button23, button24, button25, button26, button27 },
                { null, null, button28, button29, button30, null, null },
                { null, null, button31, button32, button33, null, null },
            };
        }

        // Обработчик клика по кнопке
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Начало хода
            if (clickedButton.Text == "К")
            {
                startButton = clickedButton;
                return;
            }

            // Завершение хода
            if (clickedButton.Text == "Л")
            {
                return;
            }

            // Проверка возможности хода
            if (startButton != null && clickedButton.Text == "")
            {
                endButton = clickedButton;

                // Проверка наличия пути между кнопками
                if (IsButtonsClose())
                {
                    // Обмен текстами кнопок
                    (startButton.Text, endButton.Text) = ("", "К");
                    startButton = endButton;
                    this.Refresh();

                    // Проверка условия победы
                    if (CheckVictoryCondition())
                    {
                        MessageBox.Show("Куры победили!", "Игра окончена!", MessageBoxButtons.OK);
                        Application.Exit();
                    }

                    Thread.Sleep(300);

                    // Проверка на поедание куриц
                    if (IsFoxEating())
                    {
                        if (eatenChicks >= 12)
                        {
                            MessageBox.Show("Лисы победили!", "Игра окончена!", MessageBoxButtons.OK);
                            Application.Exit();
                        }
                        return;
                    }

                    // Движение лис
                    MoveFox();
                }
            }
        }

        // Проверка наличия пути между кнопками
        private bool IsButtonsClose()
        {
            int startBtnTag = int.Parse(startButton.Tag.ToString());
            int endBtnTag = int.Parse(endButton.Tag.ToString());

            return (startBtnTag + 1 == endBtnTag) || (startBtnTag - 1 == endBtnTag) || (startBtnTag - 10 == endBtnTag);
        }

        // Проверка условия победы
        private bool CheckVictoryCondition()
        {
            return button1.Text == "К" &&
                   button2.Text == "К" &&
                   button3.Text == "К" &&
                   button4.Text == "К" &&
                   button5.Text == "К" &&
                   button6.Text == "К" &&
                   button9.Text == "К" &&
                   button10.Text == "К" &&
                   button11.Text == "К";
        }

        // Движение лис
        private void MoveFox()
        {
            int foxChoice = random.Next(1, 3);
            if (foxChoice == 1)
            {
                MoveFoxTo(ref foxField1Row, ref foxField1Column, ref foxField1);
            }
            else
            {
                MoveFoxTo(ref foxField2Row, ref foxField2Column, ref foxField2);
            }
        }

        // Проверка возможности движения лис
        private void MoveFoxTo(ref int rowFox, ref int columnFox, ref Button foxField)
        {
            Button targetFoxField = new Button();
            int count = 20;
            while (count > 0)
            {
                count--;
                try
                {
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            targetFoxField = gameButtons[rowFox - 1, columnFox];
                            if (targetFoxField != null && targetFoxField.Text == "")
                            {
                                SwapButtonsText(foxField, targetFoxField);
                                rowFox--;
                                break;
                            }
                            continue;

                        case 2:
                            targetFoxField = gameButtons[rowFox + 1, columnFox];
                            if (targetFoxField != null && targetFoxField.Text == "")
                            {
                                SwapButtonsText(foxField, targetFoxField);
                                rowFox++;
                                break;
                            }
                            continue;

                        case 3:
                            targetFoxField = gameButtons[rowFox, columnFox - 1];
                            if (targetFoxField != null && targetFoxField.Text == "")
                            {
                                SwapButtonsText(foxField, targetFoxField);
                                columnFox--;
                                break;
                            }
                            continue;

                        case 4:
                            targetFoxField = gameButtons[rowFox, columnFox + 1];
                            if (targetFoxField != null && targetFoxField.Text == "")
                            {
                                SwapButtonsText(foxField, targetFoxField);
                                columnFox++;
                                break;
                            }
                            continue;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }

                foxField = targetFoxField;
                return;
            }
        }

        // Обмен текстами между кнопками
        private static void SwapButtonsText(Button button1, Button button2)
        {
            (button1.Text, button2.Text) = (button2.Text, button1.Text);
        }

        // Проверка на поедание куриц
        private bool IsFoxEating()
        {
            bool FoxCanEat(ref Button foxField, ref int rowFoxField, ref int columnFoxField)
            {
                Button leftFoxPosition = columnFoxField -

         1 >= 0 ? gameButtons[rowFoxField, columnFoxField - 1] ?? new Button() : null;
                Button upperFoxPosition = rowFoxField - 1 >= 0 ? gameButtons[rowFoxField - 1, columnFoxField] ?? new Button() : null;
                Button lowerFoxPosition = rowFoxField + 1 <= 6 ? gameButtons[rowFoxField + 1, columnFoxField] ?? new Button() : null;
                Button rightFoxPosition = columnFoxField + 1 <= 6 ? gameButtons[rowFoxField, columnFoxField + 1] ?? new Button() : null;

                // Проверка на поедание куриц в разных направлениях
                if (leftFoxPosition != null && leftFoxPosition.Text == "К")
                {
                    if (columnFoxField - 2 >= 0 && gameButtons[rowFoxField, columnFoxField - 2]?.Text == "")
                    {
                        SwapButtonsText(gameButtons[rowFoxField, columnFoxField - 2], foxField);
                        foxField = gameButtons[rowFoxField, columnFoxField - 2];
                        leftFoxPosition.Text = "";
                        columnFoxField -= 2;
                        return true;
                    }
                }
                if (upperFoxPosition != null && upperFoxPosition.Text == "К")
                {
                    if (rowFoxField - 2 >= 0 && gameButtons[rowFoxField - 2, columnFoxField]?.Text == "")
                    {
                        SwapButtonsText(gameButtons[rowFoxField - 2, columnFoxField], foxField);
                        foxField = gameButtons[rowFoxField - 2, columnFoxField];
                        upperFoxPosition.Text = "";
                        rowFoxField -= 2;
                        return true;
                    }
                }

                // Проверка на поедание куриц в разных направлениях
                if (lowerFoxPosition != null && lowerFoxPosition.Text == "К")
                {
                    if (rowFoxField + 2 <= 6 && gameButtons[rowFoxField + 2, columnFoxField]?.Text == "")
                    {
                        SwapButtonsText(gameButtons[rowFoxField + 2, columnFoxField], foxField);
                        foxField = gameButtons[rowFoxField + 2, columnFoxField];
                        lowerFoxPosition.Text = "";
                        rowFoxField += 2;
                        return true;
                    }
                }
                if (rightFoxPosition != null && rightFoxPosition.Text == "К")
                {
                    if (columnFoxField + 2 <= 6 && gameButtons[rowFoxField, columnFoxField + 2]?.Text == "")
                    {
                        SwapButtonsText(gameButtons[rowFoxField, columnFoxField + 2], foxField);
                        foxField = gameButtons[rowFoxField, columnFoxField + 2];
                        rightFoxPosition.Text = "";
                        columnFoxField += 2;
                        return true;
                    }
                }
                return false;
            }

            int count = 0;
            while (FoxCanEat(ref foxField1, ref foxField1Row, ref foxField1Column))
            {
                count++;
                eatenChicks++;
                this.Refresh();
                Thread.Sleep(500);
            }
            if (count > 0)
                return true;
            while (FoxCanEat(ref foxField2, ref foxField2Row, ref foxField2Column))
            {
                count++;
                eatenChicks++;
                this.Refresh();
                Thread.Sleep(500);
            }
            return count > 0;
        }
    }
}