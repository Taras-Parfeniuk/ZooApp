using System;
using System.Linq;
using System.Collections.Generic;

using Repository;
using Domain.Abstract;

namespace UI
{
    public class ScreenDrawer
    {
        public string CommandMessage { get; set; }
        public string EventMessage { get; set; }

        public string ReadLineWithControl(string prefix = "")
        {
            string readString = prefix;
            int currentIndex = 0;

            do
            {
                ConsoleKeyInfo readKeyResult = Console.ReadKey(true);
                if ((readKeyResult.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    switch (readKeyResult.Key)
                    {
                        case ConsoleKey.RightArrow:
                            _table.NextPage();
                            Draw();
                            break;
                        case ConsoleKey.LeftArrow:
                            _table.PreviousPage();
                            Draw();
                            break;
                    }
                }
                switch (readKeyResult.Key)
                {
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        return readString;
                    case ConsoleKey.Backspace:
                        if (currentIndex > 0)
                        {
                            readString = readString.Remove(readString.Length - 1);
                            Console.Write(readKeyResult.KeyChar);
                            Console.Write(' ');
                            Console.Write(readKeyResult.KeyChar);
                            currentIndex--;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        currentIndex--;
                        Console.SetCursorPosition(currentIndex, Console.CursorTop);
                        break;
                    case ConsoleKey.RightArrow:
                        currentIndex++;
                        Console.SetCursorPosition(currentIndex, Console.CursorTop);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.UpArrow:
                        break;
                    default:
                        readString = readString.Insert(currentIndex, readKeyResult.KeyChar.ToString());
                        readString.Skip(currentIndex).Take(readString.Count() - currentIndex).ToList().ForEach(c => Console.Write(c));
                        currentIndex++;
                        Console.SetCursorPosition(currentIndex, Console.CursorTop);
                        break;
                }
            }
            while (true);
        }

        public void SetDataSource(IEnumerable<Animal> animals)
        {
            _table = new BindedTable<Animal>(animals, a => Tuple.Create<string, char, string>("Name|State|Health", '|', $"{a.ToString()}|{a.State}|{a.Health}/{a.MaxHealth}"));
        }

        public ScreenDrawer(IAnimalRepository animals)
        {
            Console.SetWindowSize(140, 47);
            _animals = animals;
            _showHelp = false;
            _table = new BindedTable<Animal>(_animals.GetAll(), a => Tuple.Create<string, char, string>("Name|State|Health", '|', $"{a.ToString()}|{a.State}|{a.Health}/{a.MaxHealth}"));
            _table.UsePageination();
            CommandMessage = String.Empty;
            EventMessage = String.Empty;
        }

        public void Draw()
        {
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            Console.CursorVisible = false;

            for (var i = 0; i < Console.WindowHeight - 2; i++)
            {
                CleanLine(i);
            }

            Console.SetCursorPosition(0, 0);

            Console.WriteLine("Welcome  to the Zoo!!!");
            Console.WriteLine("To exit type quit");
            Console.WriteLine("To look available commands please type help");

            Console.WriteLine("=====================================================================");
            if (_showHelp) Console.WriteLine(_help);
            Console.WriteLine("=====================================================================");

            Console.WriteLine("Animals in Zoo");
            Console.WriteLine("=====================================================================");

            _table.WriteTable();

            Console.WriteLine("=====================================================================");
            Console.WriteLine(EventMessage);
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(CommandMessage);
            Console.WriteLine("=====================================================================");

            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.CursorVisible = true;
        }

        public void DrawEventMessage(IAnimalRepository sender, RepositoryChangedEventArgs e)
        {
            EventMessage = e.Message;
            Draw();
        }

        public void ShowHelp()
        {
            _showHelp = !_showHelp;
        }

        public void CleanLine(int line)
        {
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        private const string _help = "Commands:\n"
           + " -add <new animal name, must be unique> <new animal species>\n"
           + " -remove <animal name to remove, possible to remove only dead animals>\n"
           + " -heal <animal name>\n"
           + " -feed <animal name>\n"
           + "Specieses:\n"
           + " -Wolf\n"
           + " -Bear\n"
           + " -Tiger\n"
           + " -Lion\n"
           + " -Fox\n"
           + " -Elephant\n"
           + "Page navigation:\n"
           + " -Ctrl + -> - next page\n"
           + " -Ctrl + <- - previous page\n"
           + "Lection 3 Homework methods:\n"
           + " -GetByType <type name>\n"
           + " -GetByState <state name>\n"
           + " -GetSickTigers\n"
           + " -GetElephantByName <name>\n"
           + " -GetHungryNames\n"
           + " -GetMostHelthy\n"
           + " -GetDeadCountPerType\n"
           + " -GetWolfsAndBearsByHealth - get Wolfs And Bears with health more than 3\n"
           + " -GetMinAndMaxHealthy\n"
           + " -GetHealthAverage\n"
           + " -all - get all animals\n"
           + "For hide help type help again";

        private IAnimalRepository _animals;
        private bool _showHelp;
        private BindedTable<Animal> _table;
    }
}
