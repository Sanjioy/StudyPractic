using System;

class Chessboard
{
    // Функция для проверки валидности координаты
    static bool IsCoordinateValid(char letter, int number)
    {
        // Проверяем, что буква находится в диапазоне от 'a' до 'h' и цифра от 1 до 8
        return letter >= 'a' && letter <= 'h' && number >= 1 && number <= 8;
    }

    // Функция для определения цвета поля на шахматной доске
    static bool IsSameColorField(char letter, int number)
    {
        // Определяем цвет поля на шахматной доске
        return (letter - 'a' + number) % 2 == 0;
    }

    // Функция для отрисовки шахматной доски
    static void DrawChessboard()
    {
        int boardSize = 8;

        // Отрисовываем верхние буквы
        Console.Write("  ");
        for (char letter = 'a'; letter <= 'h'; letter++)
        {
            Console.Write($"{letter}  ");
        }
        Console.WriteLine();

        // Отрисовываем шахматную доску
        for (int i = 1; i <= boardSize; i++)
        {
            // Отрисовываем цифры по левому краю доски
            Console.Write($"{i} ");

            for (char letter = 'a'; letter <= 'h'; letter++)
            {
                // Определяем цвет поля и устанавливаем цвет фона
                Console.BackgroundColor = IsSameColorField(letter, i) ? ConsoleColor.DarkGray : ConsoleColor.Magenta;
                Console.Write($"{letter}{i} ");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }

    static void Main()
    {
        // Задаем координаты двух полей, вводимые пользователем
        Console.Write("Введите координаты первого поля (например, a1): ");
        string x1y1 = Console.ReadLine();

        Console.Write("Введите координаты второго поля (например, b2): ");
        string x2y2 = Console.ReadLine();

        // Разбираем координаты на букву и цифру
        char letter1 = x1y1[0];
        int number1 = int.Parse(x1y1[1].ToString());

        char letter2 = x2y2[0];
        int number2 = int.Parse(x2y2[1].ToString());

        // Проверяем валидность введенных координат
        if (!IsCoordinateValid(letter1, number1) || !IsCoordinateValid(letter2, number2))
        {
            Console.WriteLine("Ошибка: Некорректные координаты. Введите правильные координаты.");
            Environment.Exit(1);
        }

        // Проверяем, являются ли поля одного цвета
        bool areSameColor = IsSameColorField(letter1, number1) == IsSameColorField(letter2, number2);

        // Проверяем, что пользователь не ввел одинаковые поля
        if (x1y1.Equals(x2y2, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Ошибка: Вы ввели одинаковые поля. Пожалуйста, введите разные поля.");
            Environment.Exit(1);
        }

        // Выводим результат
        Console.WriteLine($"Поля {x1y1} и {x2y2} являются полями {(areSameColor ? "одного цвета" : "разного цвета")}.");
        Console.WriteLine();

        // Рисуем шахматную доску с координатами
        DrawChessboard();
    }
}
