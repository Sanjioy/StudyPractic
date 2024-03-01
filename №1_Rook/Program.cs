using System;

class Rook
{
    // Функция для проверки корректности координат
    static bool IsValidCoordinate(string coordinate)
    {
        if (coordinate.Length != 2)
            return false;

        char file = coordinate[0];
        char rank = coordinate[1];

        return (file >= 'a' && file <= 'h') && (rank >= '1' && rank <= '8');
    }

    // Функция для проверки не находятся ли фигуры на одной клетке
    static bool AreFiguresOnSameCell(string rook, string figure)
    {
        return rook == figure;
    }

    // Функция для проверки, может ли ладья побить фигуру за один ход
    static bool CanRookCapture(string rook, string figure)
    {
        // Ладья может побить фигуру, если координаты находятся на одной линии (по горизонтали или вертикали)
        return rook[0] == figure[0] || rook[1] == figure[1];
    }

    static void Main()
    {
        // Ввод координат ладьи и фигуры
        Console.Write("Введите координаты ладьи и фигуры (например, 'a1 b3'): ");
        string input = Console.ReadLine();

        // Проверка на корректное количество координат
        if (input.Count(c => c == ' ') != 1)
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Извлечение координат ладьи и фигуры
        string[] coordinates = input.Split(' ');
        string rookPosition = coordinates[0];
        string figurePosition = coordinates[1];

        // Проверка на корректность введенных координат
        if (!IsValidCoordinate(rookPosition) || !IsValidCoordinate(figurePosition))
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Проверка не находятся ли фигуры на одной клетке
        bool areFiguresOnSameCell = AreFiguresOnSameCell(rookPosition, figurePosition);
        if (areFiguresOnSameCell)
        {
            Console.WriteLine("Фигуры находятся на одной клетке.");
            return;
        }

        // Проверка, может ли ладья побить фигуру за один ход
        bool canRookCapture = CanRookCapture(rookPosition, figurePosition);

        // Вывод результата
        Console.WriteLine(canRookCapture ? "Ладья сможет побить фигуру." : "Ладья не сможет побить фигуру.");
    }
}
