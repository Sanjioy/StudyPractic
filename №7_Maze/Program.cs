using System;
using System.Collections.Generic;
using System.IO;

// Enum для символов в лабиринте
enum MazeSymbol
{
    WallHorizontal = '═',
    WallVertical = '║',
    WallTopRight = '╣',
    WallTopLeft = '╠',
    WallTopT = '╦',
    WallBottomT = '╩',
    WallTop = '╔',
    WallBottom = '╗',
    WallBottomLeft = '╚',
    WallBottomRight = '╝',
    Finish = 'F',
    EmptySpace = ' ',
    Player = '■',
    PathDot = '.'
}

// Класс для отрисовки лабиринта
class MazeRenderer
{
    private string[] lines;

    // Конструктор класса
    public MazeRenderer(string[] mazeLines)
    {
        lines = mazeLines;
    }

    // Метод для отображения лабиринта с возможностью включения/выключения отображения маршрута
    public void RenderMaze(List<Tuple<int, int>> route, bool showDots)
    {
        Console.Clear();

        // Перебираем строки лабиринта
        for (int i = 0; i < lines.Length; i++)
        {
            char[] lineChars = lines[i].ToCharArray();

            // Перебираем символы в строке
            for (int j = 0; j < lineChars.Length; j++)
            {
                Console.SetCursorPosition(j, i);

                char currentSymbol = lineChars[j];

                // Если текущий символ точка маршрута и showDots отключен, пропускаем отображение
                if (currentSymbol == (char)MazeSymbol.PathDot && !showDots)
                {
                    continue;
                }

                Console.Write(currentSymbol);
            }
        }

        // Выводим подсказку для включения/выключения отображения маршрута
        Console.WriteLine("F1 - вкл/выкл маршрут до финиша");
    }
}

// Класс для представления врага в лабиринте
class Enemy
{
    private int enemyRow;
    private int enemyCol;

    // Конструктор класса
    public Enemy(int initialRow, int initialCol)
    {
        enemyRow = initialRow;
        enemyCol = initialCol;
    }

    // Метод для перемещения врага случайным образом
    public void MoveRandomly(char[][] maze)
    {
        Random random = new Random();
        int moveDirection = random.Next(4); // 0 - вверх, 1 - вниз, 2 - влево, 3 - вправо

        int newEnemyRow = enemyRow;
        int newEnemyCol = enemyCol;

        // Выбор направления движения и обновление координат врага
        switch (moveDirection)
        {
            case 0:
                newEnemyRow--;
                break;
            case 1:
                newEnemyRow++;
                break;
            case 2:
                newEnemyCol--;
                break;
            case 3:
                newEnemyCol++;
                break;
        }

        // Проверка на возможность движения в новые координаты
        if (IsValidMove(maze, newEnemyRow, newEnemyCol))
        {
            enemyRow = newEnemyRow;
            enemyCol = newEnemyCol;
        }
    }

    // Метод для проверки возможности движения в указанные координаты
    private bool IsValidMove(char[][] maze, int row, int col)
    {
        // Проверка, что row в пределах размеров массива
        if (row < 0 || row >= maze.Length)
        {
            return false;
        }

        // Проверка, что col в пределах размеров строки массива
        if (col < 0 || col >= maze[row].Length)
        {
            return false;
        }

        // Проверка, что клетка не является стеной
        return maze[row][col] switch
        {
            (char)MazeSymbol.WallHorizontal or
            (char)MazeSymbol.WallVertical or
            (char)MazeSymbol.WallTopRight or
            (char)MazeSymbol.WallTopLeft or
            (char)MazeSymbol.WallTopT or
            (char)MazeSymbol.WallBottomT or
            (char)MazeSymbol.WallBottomLeft or
            (char)MazeSymbol.WallBottomRight or
            (char)MazeSymbol.WallTop or
            (char)MazeSymbol.WallBottom => false,
            _ => true,
        };
    }

    // Методы для получения координат врага
    public int GetRow()
    {
        return enemyRow;
    }

    public int GetCol()
    {
        return enemyCol;
    }
}

// Класс для представления игрового процесса
class MazeGame
{
    private string[] lines;
    private int playerRow;
    private int playerCol;
    private List<Tuple<int, int>> route;
    private List<Enemy> enemies;
    private bool showDots;
    private const int MaxHealth = 100;
    private int playerHealth = MaxHealth;
    private const char HealthBarSymbol = '#';

    // Конструктор класса
    public MazeGame(string filePath)
    {
        Console.CursorVisible = false;
        lines = ReadMazeFromFile(filePath);

        playerRow = 1;
        playerCol = 5;

        route = new List<Tuple<int, int>>();
        showDots = false;

        enemies = new List<Enemy>();
        GenerateEnemies();
    }

    // Метод для создания врагов в лабиринте
    private void GenerateEnemies()
    {
        Random random = new Random();

        // Создаем случайное количество врагов (от 2 до 5, например)
        int enemyCount = random.Next(2, 6);

        for (int i = 0; i < enemyCount; i++)
        {
            int enemyRow;
            int enemyCol;

            do
            {
                // Генерируем случайные координаты для врага
                enemyRow = random.Next(lines.Length);
                enemyCol = random.Next(lines[0].Length);

                // Проверяем, что враг не появился в стене
            } while (!CanMoveTo(lines, enemyRow, enemyCol));

            // Создаем врага и добавляем его в список
            Enemy enemy = new Enemy(enemyRow, enemyCol);
            enemies.Add(enemy);
        }
    }

    // Метод для отображения лабиринта с врагами
    private void DisplayMazeWithEnemies()
    {
        Console.Clear();

        // Отображение лабиринта
        for (int i = 0; i < lines.Length; i++)
        {
            char[] lineChars = lines[i].ToCharArray();

            for (int j = 0; j < lineChars.Length; j++)
            {
                Console.SetCursorPosition(j, i);

                char currentSymbol = lineChars[j];

                // Если текущий символ точка маршрута и showDots отключен, пропускаем отображение
                if (currentSymbol == (char)MazeSymbol.PathDot && !showDots)
                {
                    continue;
                }

                Console.Write(currentSymbol);
            }
        }

        // Отображение врагов
        foreach (Enemy enemy in enemies)
        {
            Console.SetCursorPosition(enemy.GetCol(), enemy.GetRow());
            Console.Write("E");
        }

        // Отображение полосы здоровья внизу карты
        Console.SetCursorPosition(0, lines.Length);
        DisplayHealthBar();

        // Отображение подсказки
        Console.SetCursorPosition(0, lines.Length + 1);
        Console.WriteLine("F1 - вкл/выкл маршрут до финиша");
    }

    // Метод для отображения полоски здоровья
    private void DisplayHealthBar()
    {
        int healthBarLength = 10;
        int filledLength = (int)Math.Ceiling((double)playerHealth / MaxHealth * healthBarLength);

        Console.Write("[");
        for (int i = 0; i < filledLength; i++)
        {
            Console.Write(HealthBarSymbol);
        }
        for (int i = filledLength; i < healthBarLength; i++)
        {
            Console.Write("_");
        }
        Console.Write("]");
    }

    // Метод для запуска игры
    public void StartGame()
    {
        MazeRenderer mazeRenderer = new MazeRenderer(lines);

        while (true)
        {
            DisplayMazeWithEnemies();

            Console.SetCursorPosition(playerCol, playerRow);
            Console.Write((char)MazeSymbol.Player);

            MoveEnemies(lines);

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;

            Console.SetCursorPosition(playerCol, playerRow);
            Console.Write((char)MazeSymbol.EmptySpace);

            Tuple<int, int> newPosition = ProcessUserInput(key);

            // Проверка на столкновение с врагом
            if (IsPlayerTouchingEnemy(newPosition))
            {
                DisplayMazeWithEnemies();
                Console.WriteLine("Извините, вы коснулись врага. Игра завершена.");
                break;
            }

            // Проверка на достижение финиша
            if (lines[newPosition.Item1][newPosition.Item2] == (char)MazeSymbol.Finish)
            {
                DisplayMazeWithEnemies(); // Отображение лабиринта с врагами перед завершением игры
                Console.WriteLine("Поздравляем! Вы прошли лабиринт!");
                break;
            }

            // Перемещение игрока, если возможно
            if (CanMoveTo(lines, newPosition.Item1, newPosition.Item2))
            {
                MovePlayer(newPosition);
            }

            // Включение/выключение отображения маршрута
            if (key == ConsoleKey.F1)
            {
                showDots = !showDots;
            }

            // Выход из игры по нажатию клавиши Esc
            if (key == ConsoleKey.Escape)
            {
                break;
            }
        }

        Console.CursorVisible = true;
    }


    // Метод для проверки столкновения игрока с врагом
    private bool IsPlayerTouchingEnemy(Tuple<int, int> playerPosition)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];

            if (playerPosition.Item1 == enemy.GetRow() && playerPosition.Item2 == enemy.GetCol())
            {
                // Уменьшение здоровья при столкновении с врагом
                playerHealth -= 50; // Уменьшение на 50%
                if (playerHealth <= 0)
                {
                    playerHealth = 0;
                    return true; // Игрок столкнулся с врагом, и его здоровье исчерпано
                }

                // Удаление врага из списка при столкновении
                enemies.RemoveAt(i);

                return false; // Игрок столкнулся с врагом, но у него осталось здоровья
            }
        }

        return false; // Игрок не сталкивается с врагами
    }

    // Метод для чтения лабиринта из текстового файла
    private string[] ReadMazeFromFile(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            Environment.Exit(1);
            return null; // Эта строка нужна только для компиляции, код не дойдет до этой точки
        }
    }

    // Метод для обработки ввода пользователя и перемещения игрока
    private Tuple<int, int> ProcessUserInput(ConsoleKey key)
    {
        int newPlayerRow = playerRow;
        int newPlayerCol = playerCol;

        // Обработка ввода для перемещения игрока
        if (key == ConsoleKey.UpArrow && CanMoveTo(lines, playerRow - 1, playerCol))
        {
            newPlayerRow--;
        }
        else if (key == ConsoleKey.DownArrow && CanMoveTo(lines, playerRow + 1, playerCol))
        {
            newPlayerRow++;
        }
        else if (key == ConsoleKey.LeftArrow && CanMoveTo(lines, playerRow, playerCol - 1))
        {
            newPlayerCol--;
        }
        else if (key == ConsoleKey.RightArrow && CanMoveTo(lines, playerRow, playerCol + 1))
        {
            newPlayerCol++;
        }

        return new Tuple<int, int>(newPlayerRow, newPlayerCol);
    }

    // Метод для перемещения игрока и сохранения маршрута
    private void MovePlayer(Tuple<int, int> newPosition)
    {
        route.Add(newPosition);
        playerRow = newPosition.Item1;
        playerCol = newPosition.Item2;
    }

    // Метод для перемещения врагов
    private void MoveEnemies(string[] maze)
    {
        char[][] charArrayMaze = new char[maze.Length][];
        for (int i = 0; i < maze.Length; i++)
        {
            charArrayMaze[i] = maze[i].ToCharArray();
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.MoveRandomly(charArrayMaze);
        }
    }

    // Метод для проверки возможности движения на клетку с данным символом
    private bool CanMoveTo(string[] maze, int row, int col)
    {
        return row >= 0 && row < maze.Length && col >= 0 && col < maze[row].Length &&
               maze[row][col] switch
               {
                   (char)MazeSymbol.WallHorizontal or
                   (char)MazeSymbol.WallVertical or
                   (char)MazeSymbol.WallTopRight or
                   (char)MazeSymbol.WallTopLeft or
                   (char)MazeSymbol.WallTopT or
                   (char)MazeSymbol.WallBottomT or
                   (char)MazeSymbol.WallBottomLeft or
                   (char)MazeSymbol.WallBottomRight or
                   (char)MazeSymbol.WallTop or
                   (char)MazeSymbol.WallBottom => false,
                   _ => true,
               };
    }
}

// Класс с методом Main для запуска игры
class Program
{
    static void Main()
    {
        // Создание экземпляра игры и запуск
        MazeGame mazeGame = new MazeGame("C:\\Users\\sudan\\OneDrive\\Документы\\УП\\№7_Maze\\bin\\Debug\\net8.0\\map.txt");
        mazeGame.StartGame();
    }
}
