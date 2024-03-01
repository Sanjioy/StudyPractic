using System;

class Horse
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
    static bool AreFiguresOnSameCell(string knight, string figure)
    {
        return knight == figure;
    }

    // Функция для проверки, может ли конь побить фигуру за один ход
    static bool CanHorseCapture(string knight, string figure)
    {
        // Конь может побить фигуру, если разница между буквенными и цифровыми координатами соответствует ходу коня
        int fileDifference = Math.Abs(knight[0] - figure[0]);
        int rankDifference = Math.Abs(knight[1] - figure[1]);

        return (fileDifference == 1 && rankDifference == 2) || (fileDifference == 2 && rankDifference == 1);
    }

    static void Main()
    {
        // Ввод координат коня и фигуры
        Console.Write("Введите координаты коня и фигуры (например, 'a1 b3'): ");
        string input = Console.ReadLine();

        // Проверка на корректное количество координат
        if (input.Count(c => c == ' ') != 1)
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Извлечение координат коня и фигуры
        string horsePosition = input.Split(' ')[0];
        string figurePosition = input.Split(' ')[1];

        // Проверка на корректность введенных координат
        if (!IsValidCoordinate(horsePosition) || !IsValidCoordinate(figurePosition))
        {
            Console.WriteLine("Введены некорректные координаты.");
            return;
        }

        // Проверка не находятся ли фигуры на одной клетке
        bool areFiguresOnSameCell = AreFiguresOnSameCell(horsePosition, figurePosition);
        if (areFiguresOnSameCell)
        {
            Console.WriteLine("Фигуры находятся на одной клетке.");
            return;
        }

        // Проверка, может ли конь побить фигуру за один ход
        bool canHorseCapture = CanHorseCapture(horsePosition, figurePosition);

        // Вывод результата
        Console.WriteLine(canHorseCapture ? "Конь сможет побить фигуру." : "Конь не сможет побить фигуру.");
    }
}
