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
        const string CaseAddPlayer = "1";
        const string CaseBanPlayer = "2";
        const string CaseUnbanPlayer = "3";
        const string CaseDeletePlayer = "4";
        const string CaseUpdatePlayer = "5";
        const string CaseShowAllPlayers = "6";
        const string CaseExit = "7";

        static void Main(string[] args)
        {
            bool isWork = true;
            DataBase dataBase = new DataBase(); 

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine(CaseAddPlayer + ". Добавить игрока");
                Console.WriteLine(CaseBanPlayer + ". Забанить игрока");
                Console.WriteLine(CaseUnbanPlayer + ". Разбанить игрока");
                Console.WriteLine(CaseDeletePlayer + ". Удалить игрока");
                Console.WriteLine(CaseUpdatePlayer + ". Повысить уровень игрока");
                Console.WriteLine(CaseShowAllPlayers + ". Вывести всех игроков");
                Console.WriteLine(CaseExit + ". Выход");

                Console.Write("Введите команду: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case CaseAddPlayer:
                        dataBase.AddPlayer();
                        break;
                    case CaseBanPlayer:
                        dataBase.BanPlayer();
                        break;
                    case CaseUnbanPlayer:
                        dataBase.UnbanPlayer();
                        break;
                    case CaseDeletePlayer:
                        dataBase.DeletePlayer();
                        break;
                    case CaseUpdatePlayer:
                        dataBase.UpdatePlayer();
                        break;
                    case CaseShowAllPlayers:
                        dataBase.ShowAllPlayers();
                        break;
                    case CaseExit:
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

    class DataBase
    {
        const int IncorrectValue = -1;
        private int _countIdentityNumber;
        private List<Player> players = new List<Player>();

        public DataBase ()
        {
            _countIdentityNumber = 1;
        }

        private int SearchPlayerIndex()
        {
            Console.Write("Идентификационный номер игрока: ");
            int identityNumber = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].IdentityNumber == identityNumber)
                {
                    return i;
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
            BanMode(true);
        }

        public void UnbanPlayer()
        {
            BanMode(false);
        }

        private void BanMode(bool isBan)
        {
            int playerIndex = SearchPlayerIndex();
            string banMode;

            if (playerIndex != IncorrectValue)
            {
                if (isBan)
                {
                    banMode = "забанен";
                }
                else
                {
                    banMode = "разбанен";
                }

                players[playerIndex].IsBanned = isBan;
                Console.WriteLine("Игрок " + players[playerIndex].Name + " " + banMode);
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
