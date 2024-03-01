//using System;

//class Program
//{
//    static Random random = new Random();
//    static int bossHealth = 1000;
//    static int playerHealth = 500;
//    static int playerAttack = 0;
//    static bool bossCanAttack = true;
//    static bool isInvisible = false;
//    static bool inPortal = false;
//    static bool useHeal = false;
//    static int healTurns = 0;

//    static void Main()
//    {
//        Console.WriteLine("Игра начинается!");

//        // Решение, кто ходит первым
//        bool isPlayerTurn = random.Next(2) == 0;

//        while (bossHealth > 0 && playerHealth > 0)
//        {
//            if (isPlayerTurn)
//            {
//                PlayerTurn();
//            }
//            else
//            {
//                BossTurn();
//            }

//            // Переключение хода
//            isPlayerTurn = !isPlayerTurn;
//        }

//        // Вывод результата
//        if (bossHealth <= 0)
//        {
//            Console.WriteLine("Поздравляю! Вы победили!");
//        }
//        else
//        {
//            Console.WriteLine("К сожалению, вы проиграли. Попробуйте еще раз.");
//        }
//    }

//    static void PlayerTurn()
//    {
//        Console.WriteLine("\nХод игрока:");

//        // Вывод информации о состоянии игрока и босса
//        DisplayStatus();

//        // Выбор и выполнение заклинания
//        ChooseAndCastSpell();

//        // Использование хилки 
//        Heal();

//        // Уменьшение времени невидимости и портала, если активны
//        //UpdateInvisibleAndPortal();
//    }

//    static void BossTurn()
//    {
//        Console.WriteLine("\nХод босса:");

//        // Вывод информации о состоянии игрока и босса
//        DisplayStatus();

//        // Атака босса
//        BossAttack();

//        // Уменьшение времени невидимости и портала, если активны
//        //UpdateInvisibleAndPortal();

//        // Увеличение здоровья и силы атаки игрока при нахождении в портале
//        //HandlePortalEffects();

//        // Восстановление здоровья игрока после использования хилки
//        //RecoverHealthAfterHeal();
//    }

//    static void DisplayStatus()
//    {
//        Console.WriteLine($"Здоровье игрока: {playerHealth} HP");
//        Console.WriteLine($"Здоровье босса: {bossHealth} HP");
//        Console.WriteLine($"Сила атаки игрока: {playerAttack}");
//    }

//    static void ChooseAndCastSpell()
//    {
//        Console.WriteLine("Выберите заклинание:");
//        Console.WriteLine("1. Кабум");
//        Console.WriteLine("2. Инвиз");
//        Console.WriteLine("3. Портал");
//        Console.WriteLine("4. Латроп");
//        Console.WriteLine("5. Хилка");

//        int spellChoice = int.Parse(Console.ReadLine());

//        switch (spellChoice)
//        {
//            case 1:
//                CastKabum();
//                break;

//            case 2:
//                CastInvis();
//                break;

//            case 3:
//                CastPortal();
//                break;

//            case 4:
//                ExitPortal();
//                break;

//            case 5:
//                CastHeal();
//                break;

//            default:
//                Console.WriteLine("Некорректный выбор заклинания. Попробуйте снова.");
//                break;
//        }
//    }

//    static void Heal()
//    {
//        // Использование хилки
//        if (useHeal)
//        {
//            healTurns--;
//            if (healTurns == 0)
//            {
//                useHeal = false;
//                Console.WriteLine("Хилка закончилась.");
//            }
//        }
//    }

//    /*static void HandlePortalEffects()
//    {
//        // Увеличение здоровья и силы атаки игрока при нахождении в портале
//        if (inPortal)
//        {
//            playerHealth += 20;
//            playerAttack += 30;
//            Console.WriteLine($"В портале: +20 HP и +30 силы атаки.");
//        }
//    }*/

//    /*static void RecoverHealthAfterHeal()
//    {
//        // Восстановление здоровья игрока после использования хилки
//        if (useHeal)
//        {
//            playerHealth += 50;
//            Console.WriteLine($"Восстановление здоровья: +50 HP.");
//        }
//    }*/

//   static void CastKabum()
//    {
//        playerAttack = random.Next(50, 101);
//        bossHealth -= playerAttack;
//        //playerAttack += 10;
//    }

//    static void CastInvis()
//    {
//        if ((!inPortal) && (!isInvisible) && playerHealth > 100)
//        {
//            isInvisible = true;
//            playerHealth -= 60;
//            for (int i = 0; i < 3; i++)
//            {
//                playerHealth += 10;
//                bossHealth -= 60;
//                Console.WriteLine($"При невидимости: +10 HP и -60 HP босса. Остался(-ось) {2 - i} ход(-а).");
//                //bossCanAttack = false;
//            }
//        }
//        else if (isInvisible)
//        {
//            Console.WriteLine("Вы уже невидимы.");
//        }
//        else if (inPortal)
//        {
//            Console.WriteLine("В портале нельзя стать невидимым.");
//        }
//        else
//        {
//            Console.WriteLine("Недостаточно здоровья для использования инвиза.");
//        }
//    }

//    static void CastPortal()
//    {
//        if ((!isInvisible) && (!inPortal) && playerHealth > 100)
//        {
//            inPortal = true;
//            playerHealth -= 100;
//            for (int i = 0; i < 5; i++)
//            {
//                playerHealth += 20;
//                playerAttack += 30;
//                Console.WriteLine($"В портале: +20 HP и +30 силы атаки. Остался(-ось) {4 - i} ход(-а).");
//                bossCanAttack = false;
//            }
//        }
//        else if (inPortal)
//        {
//            Console.WriteLine("Вы уже находитесь в портале.");
//        }
//        else if (isInvisible)
//        {
//            Console.WriteLine("В портал нельзя зайти невидимым.");
//        }
//        else
//        {
//            Console.WriteLine("Недостаточно здоровья для использования портала.");
//        }
//    }

//    static void ExitPortal()
//    {
//        if (inPortal)
//        {
//            inPortal = false;
//            Console.WriteLine("Вы вышли из портала.");
//        }
//        else
//        {
//            Console.WriteLine("Вы не находитесь в портале.");
//        }
//    }

//    static void CastHeal()
//    {
//        healTurns = 3;
//        playerHealth += 50;
//        Console.WriteLine($"Хилка: +50 HP.");
//    }

//    static void BossAttack()
//    {
//        if (bossCanAttack)
//        {
//            int bossAttack = random.Next(80, 121);
//            playerHealth -= bossAttack;
//            Console.WriteLine($"Босс атакует вас и наносит {bossAttack} урона.");
//        }
//    }
//}

using System;

class Program
{
    // Инициализация переменных для здоровья игрока и босса, а также статусов заклинаний
    static int playerHealth = 750;
    static int bossHealth = 1500;
    static bool iceProtectionActive = false;
    static bool shadowOfTimeActive = false;
    static bool ghostHealingActive = false;
    static bool previousActionWasIceProtection = false;

    // Основной метод для выбора определенного заклинания
    static void Main()
    {
        Console.WriteLine("Босс появился! Начинаем бой!");

        // Цикл боя, продолжается, пока здоровье игрока и босса положительно
        while (playerHealth > 0 && bossHealth > 0)
        {
            Console.WriteLine($"Текущее здоровье игрока: {playerHealth}");
            Console.WriteLine($"Текущее здоровье босса: {bossHealth}");
            Console.WriteLine();
            Console.WriteLine("Выберите заклинание:");
            Console.WriteLine("1. Ice Protection - уменьшает получаемый урон на 30% на следующем ходу.");
            Console.WriteLine("2. GhostHealing - восстанавливает 150 HP и наносит 50 урона боссу. Босс не может атаковать.");
            Console.WriteLine("3. Theft - украдите здоровье у босса и добавьте 30% отнятого здоровья к своему, если предыдущее заклинание было Ice Protection.");
            Console.WriteLine("4. Shadow Of Time - предотвращает атаку босса в следующем раунде.");
            Console.WriteLine("5. Radiant Storm - наносит 180 урона боссу.");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    IceProtection();
                    break;

                case 2:
                    GhostHealing();
                    break;

                case 3:
                    Theft();
                    break;

                case 4:
                    ShadowOfTime();
                    break;

                case 5:
                    RadiantStorm();
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Пропуск хода.");
                    break;
            }

            // Атака босса после выбора заклинания
            BossAttack();
        }

        // Вывод результата битвы
        if (playerHealth <= 0)
        {
            Console.WriteLine("Вы проиграли.");
        }
        else
        {
            Console.WriteLine("Вы победили босса.");
        }
    }

    // Заклинания
    static void IceProtection()
    {
        Console.WriteLine("Вы создали Ice Protection. Получаемый урон уменьшен на 30% на следующем ходу.");
        iceProtectionActive = true;
        previousActionWasIceProtection = true;
    }
    static void GhostHealing()
    {
        Console.WriteLine($"Вы использовали Ghost Healing, и восстанавили 150 HP и нанесли 50 урона боссу.");
        playerHealth += 150;
        bossHealth -= 50;
        ghostHealingActive = true;
        previousActionWasIceProtection = false;
    }
    static void Theft()
    {
        if (previousActionWasIceProtection)
        {
            Console.WriteLine("Вы использовали Theft и украли здоровье у босса.");
            int stolenHealth = (int)(bossHealth * 0.3);
            playerHealth += stolenHealth;
            bossHealth -= stolenHealth;
            iceProtectionActive = false;
        }
        else
        {
            Console.WriteLine("Вы не можете использовать Theft без предыдущего заклинания Ice Protection.");
        }
        previousActionWasIceProtection = false;
    }
    static void ShadowOfTime()
    {
        Console.WriteLine("Вы окружили себя Shadow Of Time, предотвращая атаку босса в следующем раунде.");
        shadowOfTimeActive = true;
        previousActionWasIceProtection = false;
    }
    static void RadiantStorm()
    {
        int damage = 180;
        Console.WriteLine($"Вы вызвали Radiant Storm и нанесли боссу {damage} урона.");
        bossHealth -= damage;
        previousActionWasIceProtection = false;
    }

    // Атака босса
    static void BossAttack()
    {
        // Проверки статусов и атака босса
        if (!shadowOfTimeActive)
        {
            if (!ghostHealingActive)
            {
                int bossDamage = 150;

                if (iceProtectionActive)
                {
                    Console.WriteLine("Босс атакует вас, но урон уменьшен на 30% из-за Ice Protection.");
                    bossDamage -= (bossDamage * 30 / 100);
                    iceProtectionActive = false;
                }
                else
                {
                    Console.WriteLine($"Босс атакует вас и наносит {bossDamage} урона.");
                }

                playerHealth -= bossDamage;
            }
            else
            {
                Console.WriteLine("Босс не атакует из-за эффекта Ghost Healing.");
                ghostHealingActive = false;
            }
        }
        else
        {
            Console.WriteLine("Босс не атакует из-за эффекта Shadow Of Time.");
            shadowOfTimeActive = false;
        }
    }
}


