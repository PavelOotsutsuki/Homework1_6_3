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
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public Player(int identityNumber, string name, int level=1, bool isBanned=false)
        {
            IdentityNumber = identityNumber;
            Name = name;
            Level = level;
            IsBanned = isBanned;
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }

        public void Update()
        {
            Level++;
        }
    }

    class Database
    {
        private int _countIdentityNumber;
        private List<Player> players = new List<Player>();

        public Database ()
        {
            _countIdentityNumber = 1;
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

        private bool TryGetPlayer(out Player player)
        {
            Console.Write("Идентификационный номер игрока: ");

            if (int.TryParse(Console.ReadLine(), out int identityNumber))
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].IdentityNumber == identityNumber)
                    {
                        player = players[i];
                        return true;
                    }
                }
            }

            Console.WriteLine("Игрок не найден");
            player = null;
            return false;
        }

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player bannedPlayer))
            {
                bannedPlayer.Ban();
                Console.WriteLine("Игрок " + bannedPlayer.Name + " забанен");
            }
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player unbannedPlayer))
            {
                unbannedPlayer.Unban();
                Console.WriteLine("Игрок " + unbannedPlayer.Name + " разбанен");
            }
        }

        public void UpdatePlayer()
        {
            if (TryGetPlayer(out Player updatedPlayer))
            {
                updatedPlayer.Update();
                Console.WriteLine("Уровень игрока " + updatedPlayer.Name + " - " + updatedPlayer.Level);
            }
        }

        public void DeletePlayer()
        {
            if (TryGetPlayer(out Player deletedPlayer))
            {
                Console.WriteLine("Игрок " + deletedPlayer.Name + " удален");
                players.Remove(deletedPlayer);
            }
        }
    }
}
