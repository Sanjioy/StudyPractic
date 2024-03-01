using System;

class ChessCoord
{
    static void Main()
    {
        // Пользователь вводит название фигуры (ладья, слон, король, ферзь).
        Console.Write("Введите название фигуры (ладья, слон, король, ферзь): ");
        string figure = Console.ReadLine();

        // Генерация случайных координат для x1y1 и x2y2, удостоверившись, что они не одинаковы.
        string x1y1, x2y2;

        do
        {
            Random random = new Random();
            x1y1 = GetRandomCoordinate(random);
            x2y2 = GetRandomCoordinate(random);
        } while (x1y1 == x2y2);

        // Вывод координат полей.
        Console.WriteLine($"Координаты первого поля (x1y1): {x1y1}");
        Console.WriteLine($"Координаты второго поля (x2y2): {x2y2}");

        // Проверка условий в зависимости от выбранной фигуры.
        switch (figure.ToLower())
        {
            case "ладья":
                CheckRook(x1y1, x2y2);
                break;

            case "слон":
                CheckBishop(x1y1, x2y2);
                break;

            case "король":
                CheckKing(x1y1, x2y2);
                break;

            case "ферзь":
                CheckQueen(x1y1, x2y2);
                break;

            default:
                Console.WriteLine("Неверное название фигуры. Программа завершена.");
                Environment.Exit(0);
                break;
        }

        // Отрисовка шахматной доски с расположенными фигурами.
        DrawChessBoard(x1y1, x2y2);
    }

    // Метод для получения случайных координат.
    static string GetRandomCoordinate(Random random)
    {
        char file = (char)('a' + random.Next(8)); // Случайная буква от a до h.
        int rank = random.Next(1, 9); // Случайная цифра от 1 до 8.
        return $"{file}{rank}";
    }

    // Метод для отрисовки шахматной доски.
    static void DrawChessBoard(string x1y1, string x2y2)
    {
        Console.WriteLine("Шахматная доска:");

        Console.Write("  ");
        for (char letter = 'a'; letter <= 'h'; letter++)
        {
            Console.Write($"{letter}  ");
        }
        Console.WriteLine();

        for (int row = 8; row >= 1; row--)
        {
            Console.Write($"{row} ");

            for (char col = 'a'; col <= 'h'; col++)
            {
                string position = $"{col}{row}";

                if (position == x1y1)
                    Console.BackgroundColor = ConsoleColor.Green; // Первая фигура
                else if (position == x2y2)
                    Console.BackgroundColor = ConsoleColor.Red; // Вторая фигура
                else
                    Console.BackgroundColor = (col - 'a' + row) % 2 == 0 ? ConsoleColor.DarkGray : ConsoleColor.Magenta;

                Console.Write($"{position} ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    // Метод для проверки ладьи.
    static void CheckRook(string x1y1, string x2y2)
    {
        char file1 = x1y1[0];
        int rank1 = int.Parse(x1y1[1].ToString());
        char file2 = x2y2[0];
        int rank2 = int.Parse(x2y2[1].ToString());

        if (file1 != file2 && rank1 != rank2)
            Console.WriteLine("Ладья не угрожает второму полю.");
        else
            Console.WriteLine("Ладья угрожает второму полю.");
    }

    // Метод для проверки слона.
    static void CheckBishop(string x1y1, string x2y2)
    {
        char file1 = x1y1[0];
        int rank1 = int.Parse(x1y1[1].ToString());
        char file2 = x2y2[0];
        int rank2 = int.Parse(x2y2[1].ToString());

        if (Math.Abs(file1 - file2) != Math.Abs(rank1 - rank2))
            Console.WriteLine("Слон не угрожает второму полю.");
        else
            Console.WriteLine("Слон угрожает второму полю.");
    }

    // Метод для проверки короля.
    static void CheckKing(string x1y1, string x2y2)
    {
        char file1 = x1y1[0];
        int rank1 = int.Parse(x1y1[1].ToString());
        char file2 = x2y2[0];
        int rank2 = int.Parse(x2y2[1].ToString());

        if (Math.Abs(file1 - file2) <= 1 && Math.Abs(rank1 - rank2) <= 1)
            Console.WriteLine("Король может попасть на второе поле одним ходом.");
        else
            Console.WriteLine("Король не может попасть на второе поле одним ходом.");
    }

    // Метод для проверки ферзя.
    static void CheckQueen(string x1y1, string x2y2)
    {
        char file1 = x1y1[0];
        int rank1 = int.Parse(x1y1[1].ToString());
        char file2 = x2y2[0];
        int rank2 = int.Parse(x2y2[1].ToString());

        if (file1 != file2 && rank1 != rank2 && Math.Abs(file1 - file2) != Math.Abs(rank1 - rank2))
            Console.WriteLine("Ферзь не угрожает второму полю.");
        else
            Console.WriteLine("Ферзь угрожает второму полю.");
    }

}
