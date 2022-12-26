using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework1_6_3
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandCaseAddPlayer = "1";
            const string CommandCaseBanPlayer = "2";
            const string CommandCaseUnbanPlayer = "3";
            const string CommandCaseDeletePlayer = "4";
            const string CommandCaseUpdatePlayer = "5";
            const string CommandCaseShowAllPlayers = "6";
            const string CommandCaseExit = "7";

            bool isWork = true;
            Database database = new Database(); 

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine(CommandCaseAddPlayer + ". Добавить игрока");
                Console.WriteLine(CommandCaseBanPlayer + ". Забанить игрока");
                Console.WriteLine(CommandCaseUnbanPlayer + ". Разбанить игрока");
                Console.WriteLine(CommandCaseDeletePlayer + ". Удалить игрока");
                Console.WriteLine(CommandCaseUpdatePlayer + ". Повысить уровень игрока");
                Console.WriteLine(CommandCaseShowAllPlayers + ". Вывести всех игроков");
                Console.WriteLine(CommandCaseExit + ". Выход");

                Console.Write("Введите команду: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandCaseAddPlayer:
                        database.AddPlayer();
                        break;

                    case CommandCaseBanPlayer:
                        database.BanPlayer();
                        break;

                    case CommandCaseUnbanPlayer:
                        database.UnbanPlayer();
                        break;

                    case CommandCaseDeletePlayer:
                        database.DeletePlayer();
                        break;

                    case CommandCaseUpdatePlayer:
                        database.UpdatePlayer();
                        break;

                    case CommandCaseShowAllPlayers:
                        database.ShowAllPlayers();
                        break;

                    case CommandCaseExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Введена неверная команда");
                        break;
                }

                Console.ReadKey();
            }
        }
    }

    class Player
    {
        public int IdentityNumber { get; private set; }
        public string Name { get; private set; }
        public  int Level { get; set; }
        public  bool IsBanned { get; set; }

        public Player(int identityNumber, string name, int level=1, bool isBanned=false)
        {
            IdentityNumber = identityNumber;
            Name = name;
            Level = level;
            IsBanned = isBanned;
        }
    }

    class Database
    {
        const int IncorrectValue = -1;

        private int _countIdentityNumber;
        private List<Player> players = new List<Player>();

        public Database ()
        {
            _countIdentityNumber = 1;
        }

        private int SearchPlayerIndex()
        {
            Console.Write("Идентификационный номер игрока: ");

            if (int.TryParse(Console.ReadLine(), out int identityNumber))
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].IdentityNumber == identityNumber)
                    {
                        return i;
                    }
                }
            }

            Console.WriteLine("Игрок не найден");
            return IncorrectValue;
        }


        public void AddPlayer()
        {
            Console.WriteLine("Введите ник: ");
            string name = Console.ReadLine();

            players.Add(new Player(_countIdentityNumber, name));

            _countIdentityNumber++;
            Console.WriteLine("Игрок успешно добавлен");
        }

        public void ShowAllPlayers()
        {
            int playersCounter = 1;

            foreach (var player in players)
            {
                Console.WriteLine("\nИгрок " + playersCounter + ":\n");
                Console.WriteLine("Идентификационный номер игрока: " + player.IdentityNumber);
                Console.WriteLine("Ник: " + player.Name);
                Console.WriteLine("Уровень: " + player.Level);

                if (player.IsBanned)
                {
                    Console.WriteLine("Забанен");
                }
                else
                {
                    Console.WriteLine("Не забанен");
                }

                playersCounter++;
            }
        }

        public void BanPlayer()
        {
            int playerIndex = SearchPlayerIndex();
            bool isBan = true;

            if (playerIndex != IncorrectValue)
            {
                players[playerIndex].IsBanned = isBan;
                Console.WriteLine("Игрок " + players[playerIndex].Name + " забанен");
            }
        }

        public void UnbanPlayer()
        {
            int playerIndex = SearchPlayerIndex();
            bool isBan = false;

            if (playerIndex != IncorrectValue)
            {
                players[playerIndex].IsBanned = isBan;
                Console.WriteLine("Игрок " + players[playerIndex].Name + " разбанен");
            }
        }

        public void UpdatePlayer()
        {
            int playerIndex = SearchPlayerIndex();

            if (playerIndex != IncorrectValue)
            {
                players[playerIndex].Level++;
                Console.WriteLine("Уровень игрока " + players[playerIndex].Name + " - " + players[playerIndex].Level);
            }
        }

        public void DeletePlayer()
        {
            int playerIndex = SearchPlayerIndex();

            if (playerIndex != IncorrectValue)
            {
                Console.WriteLine("Игрок " + players[playerIndex].Name + " удален");
                players.RemoveAt(playerIndex);
            }
        }
    }
}
