using System;

class ChessBoard
{
    static void Main()
    {
        Console.WriteLine("Введите исходные данные (ферзь d3 слон e1 d8): ");
        string inputData = Console.ReadLine();
        string[] inputParts = inputData.Split(' ');

        string whitePiece = inputParts[0];
        string whitePieceCoords = inputParts[1];
        string blackPiece = inputParts[2];
        string blackPieceCoords = inputParts[3];
        string targetCoords = inputParts[4];

        bool canWhiteReachTarget = CanPieceReachTarget(whitePiece, whitePieceCoords, blackPiece, blackPieceCoords, targetCoords);

        Console.WriteLine($"Результат: {(canWhiteReachTarget ? $"{whitePiece} дойдет до {targetCoords}" : $"{whitePiece} не сможет дойти до {targetCoords}")}.");

        if (canWhiteReachTarget)
        {
            Console.WriteLine($"Последовательность ходов белой фигуры: {GetMoveSequence(whitePiece, whitePieceCoords, targetCoords)}");
            DrawChessBoard(whitePieceCoords, blackPieceCoords, targetCoords);
        }
    }

    static string GetMoveSequence(string piece, string startCoords, string targetCoords)
    {
        int startX = GetXCoordinate(startCoords);
        int startY = GetYCoordinate(startCoords);
        int targetX = GetXCoordinate(targetCoords);
        int targetY = GetYCoordinate(targetCoords);

        string moveNotation = $"{piece} {ConvertToChessNotation(startX, startY)}";

        if (startX != targetX || startY != targetY)
        {
            moveNotation += $" - {ConvertToChessNotation(targetX, targetY)}";
        }

        return moveNotation;
    }

    static string ConvertToChessNotation(int x, int y)
    {
        char file = (char)('a' + x - 1);
        return $"{file}{y}";
    }

    static void DrawChessBoard(string whitePieceCoords, string blackPieceCoords, string targetCoords)
    {
        Console.WriteLine("Шахматная доска:");

        int boardSize = 8;

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

                if (position == whitePieceCoords)
                    Console.BackgroundColor = ConsoleColor.Green; // Белая фигура
                else if (position == blackPieceCoords)
                    Console.BackgroundColor = ConsoleColor.Red; // Черная фигура
                else if (position == targetCoords)
                    Console.BackgroundColor = ConsoleColor.Yellow; // Целевая клетка
                else
                    Console.BackgroundColor = (col - 'a' + row) % 2 == 0 ? ConsoleColor.DarkGray : ConsoleColor.Magenta;

                Console.Write($"{position} ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    static int GetXCoordinate(string coords)
    {
        return coords[0] - 'a' + 1;
    }

    static int GetYCoordinate(string coords)
    {
        try
        {
            return int.Parse(coords[1].ToString());
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: некорректный ввод координат Y. Введите число от 1 до 8.");
            return GetYCoordinate(Console.ReadLine()); // Повторный ввод
        }
    }

    static bool CanPieceReachTarget(string whitePiece, string whitePieceCoords, string blackPiece, string blackPieceCoords, string targetCoords)
    {
        int whiteX = GetXCoordinate(whitePieceCoords);
        int whiteY = GetYCoordinate(whitePieceCoords);
        int blackX = GetXCoordinate(blackPieceCoords);
        int blackY = GetYCoordinate(blackPieceCoords);
        int targetX = GetXCoordinate(targetCoords);
        int targetY = GetYCoordinate(targetCoords);

        switch (whitePiece.ToLower())
        {
            case "ладья":
                return CanRookReachTarget(whiteX, whiteY, blackX, blackY, targetX, targetY);
            case "конь":
                return CanKnightReachTarget(whiteX, whiteY, blackX, blackY, targetX, targetY);
            case "слон":
                return CanBishopReachTarget(whiteX, whiteY, blackX, blackY, targetX, targetY);
            case "ферзь":
                return CanQueenReachTarget(whiteX, whiteY, blackX, blackY, targetX, targetY);
            case "король":
                return CanKingReachTarget(whiteX, whiteY, blackX, blackY, targetX, targetY);
            default:
                Console.WriteLine($"Неизвестная фигура: {whitePiece}");
                return false;
        }
    }

    static bool CanRookReachTarget(int whiteX, int whiteY, int blackX, int blackY, int targetX, int targetY)
    {
        // Ладья движется по вертикали или горизонтали
        return whiteX == targetX || whiteY == targetY;
    }

    static bool CanKnightReachTarget(int whiteX, int whiteY, int blackX, int blackY, int targetX, int targetY)
    {
        // Конь ходит буквой "Г"
        int dx = Math.Abs(targetX - whiteX);
        int dy = Math.Abs(targetY - whiteY);
        return (dx == 1 && dy == 2) || (dx == 2 && dy == 1);
    }

    static bool CanBishopReachTarget(int whiteX, int whiteY, int blackX, int blackY, int targetX, int targetY)
    {
        // Слон ходит по диагонали
        return Math.Abs(targetX - whiteX) == Math.Abs(targetY - whiteY);
    }

    static bool CanQueenReachTarget(int whiteX, int whiteY, int blackX, int blackY, int targetX, int targetY)
    {
        // Ферзь объединяет ходы ладьи и слона
        return CanRookReachTarget(whiteX, whiteY, blackX, blackY, targetX, targetY) || CanBishopReachTarget(whiteX, whiteY, blackX, blackY, targetX, targetY);
    }

    static bool CanKingReachTarget(int whiteX, int whiteY, int blackX, int blackY, int targetX, int targetY)
    {
        // Король ходит на одну клетку в любом направлении
        int dx = Math.Abs(targetX - whiteX);
        int dy = Math.Abs(targetY - whiteY);
        return dx <= 1 && dy <= 1;
    }
}