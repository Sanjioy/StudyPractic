using System;

class Queen
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
    static bool AreFiguresOnSameCell(string queen, string figure)
    {
        return queen == figure;
    }

    // Функция для проверки, может ли ферзь побить фигуру за один ход
    static bool CanQueenCapture(string queen, string figure)
    {
        // Ферзь может побить фигуру, если координаты находятся на одной линии (по горизонтали, вертикали или диагонали)
        int fileDifference = Math.Abs(queen[0] - figure[0]);
        int rankDifference = Math.Abs(queen[1] - figure[1]);

        return queen[0] == figure[0] || queen[1] == figure[1] || fileDifference == rankDifference;
    }

    static void Main()
    {
        // Ввод координат ферзя и фигуры
        Console.Write("Введите координаты ферзя и фигуры (например, 'a1 b3'): ");
        string input = Console.ReadLine();

        // Проверка на корректное количество координат
        if (input.Count(c => c == ' ') != 1)
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Извлечение координат ферзя и фигуры
        string queenPosition = input.Split(' ')[0];
        string figurePosition = input.Split(' ')[1];

        // Проверка на корректность введенных координат
        if (!IsValidCoordinate(queenPosition) || !IsValidCoordinate(figurePosition))
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Проверка не находятся ли фигуры на одной клетке
        bool areFiguresOnSameCell = AreFiguresOnSameCell(queenPosition, figurePosition);
        if (areFiguresOnSameCell)
        {
            Console.WriteLine("Фигуры находятся на одной клетке.");
            return;
        }

        // Проверка, может ли ферзь побить фигуру за один ход
        bool canQueenCapture = CanQueenCapture(queenPosition, figurePosition);

        // Вывод результата
        Console.WriteLine(canQueenCapture ? "Ферзь сможет побить фигуру." : "Ферзь не сможет побить фигуру.");
    }
}
