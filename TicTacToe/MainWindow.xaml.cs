using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// Импорты можно оставить без изменений.

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        #region Private Members

        // Содержит текущие значения ячеек.
        private MarkType[] mResults;

        // Чей ход.
        private bool mPlayer1Turn;

        // Закончилась ли игра.
        private bool mGameEnded;

        // Счетчики выигранных матчей для каждого игрока.
        private int mPlayer1Wins;
        private int mPlayer2Wins;

        #endregion

        #region Constructor

        public MainWindow()
        {
            // Инициализация счетчиков при запуске приложения.
            mPlayer1Wins = 0;
            mPlayer2Wins = 0;

            InitializeComponent();

            NewGame();
        }

        #endregion

        // Вывод счетчиков на экран.
        private void UpdateScoreboard()
        {
            // Обновление текста счетчиков в соответствии с текущими значениями.
            (Container.Children.OfType<TextBlock>().First() as TextBlock).Text = $"Игрок 1: {mPlayer1Wins}";
            (Container.Children.OfType<TextBlock>().Last() as TextBlock).Text = $"Игрок 2: {mPlayer2Wins}";
        }

        // Запуск новой игры и возвращение всех значений к исходным.
        private void NewGame()
        {
            // Создание массива под ячейки.
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            // Является ли первый игрок текущим.
            mPlayer1Turn = true;

            // Встроить каждую кнопку в сетку.
            Container.Children.OfType<Button>().ToList().ForEach(button =>
            {
                // Изменение значений по умолчанию.
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Убедиться, что игра не закончилась.
            mGameEnded = false;
        }

        // Нажатие кнопки.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Привязать отправителя к кнопке.
            var button = (Button)sender;

            // Нахождение позиции кнопки в массиве.
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Ничего не делать, если в ячейке уже есть значение.
            if (mResults[index] != MarkType.Free)
            {
                return;
            }

            // Устанавливать значение ячейки, исходя чей ход.
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Установить текст в кнопке.
            button.Content = mPlayer1Turn ? "X" : "O";

            // Изменить цвет "O" на красный.
            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            // Переключение игроков по очереди.
            mPlayer1Turn ^= true;

            // Кто победил.
            CheckForWinner();

            if (mGameEnded)
            {
                // Вывести сообщение о победе одного из игроков.
                if (!mPlayer1Turn)
                {
                    MessageBox.Show("Игрок 1 (X) победил!", "Игра окончена");
                    mPlayer1Wins++;
                }
                else
                {
                    MessageBox.Show("Игрок 2 (O) победил!", "Игра окончена");
                    mPlayer2Wins++;
                }

                // Обновление текстовых блоков счетчиков.
                UpdateScoreboard();

                NewGame();
            }

            // Проверка на ничью.
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;
                MessageBox.Show("Ничья!", "Игра окончена");

                NewGame();
            }
        }

        // Есть ли победитель на 3 линиях.
        private void CheckForWinner()
        {
            // Проверка на победу.
            if (CheckHorizontalWins() || CheckVerticalWins() || CheckDiagonalWins())
            {
                mGameEnded = true;

                // Вывод сообщения о победе и обновление счетчиков.
                if (!mPlayer1Turn)
                {
                    MessageBox.Show("Игрок 1 (X) победил!", "Игра окончена");
                    mPlayer1Wins++;
                }
                else
                {
                    MessageBox.Show("Игрок 2 (O) победил!", "Игра окончена");
                    mPlayer2Wins++;
                }

                UpdateScoreboard();

                NewGame();

                return;
            }

            // Проверка на ничью после заполнения всех ячеек.
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;

                // Окрашивание всех ячеек в оранжевый цвет.
                foreach (var button in Container.Children.OfType<Button>())
                {
                    button.Background = Brushes.Orange;
                }
                MessageBox.Show("Ничья!", "Игра окончена");

                NewGame();

                return;
            }
        }

        // Проверка на победу по горизонтали.
        private bool CheckHorizontalWins()
        {
            // Строка 0.
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Игра окончена.
                mGameEnded = true;

                // Выделить выигрышные ячейки зеленым.
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

                return true;
            }

            // Строка 1.
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Игра окончена.
                mGameEnded = true;

                // Выделить выигрышные ячейки зеленым.
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

                return true;
            }

            // Строка 2.
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Игра окончена.
                mGameEnded = true;

                // Выделить выигрышные ячейки зеленым.
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

                return true;
            }

            return false;
        }

        // Проверка на победу по вертикали.
        private bool CheckVerticalWins()
        {
            // Столбец 0.
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Игра окончена.
                mGameEnded = true;

                // Выделить выигрышные ячейки зеленым.
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

                return true;
            }

            // Столбец 1.
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Игра окончена.
                mGameEnded = true;

                // Выделить выигрышные ячейки зеленым.
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

                return true;
            }

            // Столбец 2.
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Игра окончена.
                mGameEnded = true;

                // Выделить выигрышные ячейки зеленым.
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

                return true;
            }

            return false;
        }

        // Проверка на победу по диагонали.
        private bool CheckDiagonalWins()
        {
            // Верхний левый - нижний правый.
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Игра окончена.
                mGameEnded = true;

                // Выделить выигрышные ячейки зеленым.
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

                return true;
            }

            // Верхний правый - нижний левый.
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Игра окончена.
                mGameEnded = true;

                // Выделить выигрышные ячейки зеленым.
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

                return true;
            }

            return false;
        }
    }
}
