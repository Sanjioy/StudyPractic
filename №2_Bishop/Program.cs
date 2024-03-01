using System;

class Bishop
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
    static bool AreFiguresOnSameCell(string bishop, string figure)
    {
        return bishop == figure;
    }

    // Функция для проверки, может ли слон побить фигуру за один ход
    static bool CanBishopCapture(string bishop, string figure)
    {
        // Слон может побить фигуру, если разница между буквенными и цифровыми координатами одинакова
        int fileDifference = Math.Abs(bishop[0] - figure[0]);
        int rankDifference = Math.Abs(bishop[1] - figure[1]);

        return fileDifference == rankDifference;
    }

    static void Main()
    {
        // Ввод координат слона и фигуры
        Console.Write("Введите координаты слона и фигуры (например, 'a1 b3'): ");
        string input = Console.ReadLine();

        // Проверка на корректное количество координат
        if (input.Count(c => c == ' ') != 1)
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Извлечение координат слона и фигуры
        string[] coordinates = input.Split(' ');
        string bishopPosition = coordinates[0];
        string figurePosition = coordinates[1];

        // Проверка на корректность введенных координат
        if (!IsValidCoordinate(bishopPosition) || !IsValidCoordinate(figurePosition))
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Проверка не находятся ли фигуры на одной клетке
        bool areFiguresOnSameCell = AreFiguresOnSameCell(bishopPosition, figurePosition);
        if (areFiguresOnSameCell)
        {
            Console.WriteLine("Фигуры находятся на одной клетке.");
            return;
        }

        // Проверка, может ли слон побить фигуру за один ход
        bool canBishopCapture = CanBishopCapture(bishopPosition, figurePosition);

        // Вывод результата
        Console.WriteLine(canBishopCapture ? "Слон сможет побить фигуру." : "Слон не сможет побить фигуру.");
    }
}
