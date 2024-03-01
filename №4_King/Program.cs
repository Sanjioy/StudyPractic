using System;

class King
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
    static bool AreFiguresOnSameCell(string king, string figure)
    {
        return king == figure;
    }

    // Функция для проверки, может ли король побить фигуру за один ход
    static bool CanKingCapture(string king, string figure)
    {
        // Король может побить фигуру, если разница между буквенными и цифровыми координатами не превышает 1
        int fileDifference = Math.Abs(king[0] - figure[0]);
        int rankDifference = Math.Abs(king[1] - figure[1]);

        return fileDifference <= 1 && rankDifference <= 1;
    }

    static void Main()
    {
        // Ввод координат короля и фигуры
        Console.Write("Введите координаты короля и фигуры (например, 'a1 b3'): ");
        string input = Console.ReadLine();

        // Проверка на корректное количество координат
        if (input.Count(c => c == ' ') != 1)
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Извлечение координат короля и фигуры
        string kingPosition = input.Split(' ')[0];
        string figurePosition = input.Split(' ')[1];

        // Проверка на корректность введенных координат
        if (!IsValidCoordinate(kingPosition) || !IsValidCoordinate(figurePosition))
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Проверка не находятся ли фигуры на одной клетке
        bool areFiguresOnSameCell = AreFiguresOnSameCell(kingPosition, figurePosition);
        if (areFiguresOnSameCell)
        {
            Console.WriteLine("Фигуры находятся на одной клетке.");
            return;
        }

        // Проверка, может ли король побить фигуру за один ход
        bool canKingCapture = CanKingCapture(kingPosition, figurePosition);

        // Вывод результата
        Console.WriteLine(canKingCapture ? "Король сможет побить фигуру." : "Король не сможет побить фигуру.");
    }
}
