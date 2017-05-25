using System;

using Repository;

namespace UI
{
    public class ScreenDrawer
    {
        public string CommandMessage { get; set; }
        public string EventMessage { get; set; }

        public string ReadLineOrEsc(string prefix = "")
        {
            string retString = prefix;
            int curIndex = 0;

            do
            {
                ConsoleKeyInfo readKeyResult = Console.ReadKey(true);

                if ((readKeyResult.Key == ConsoleKey.RightArrow || readKeyResult.Key == ConsoleKey.LeftArrow) 
                    && (readKeyResult.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    
                }

                // handle Enter
                if (readKeyResult.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return retString;
                }

                // handle backspace
                if (readKeyResult.Key == ConsoleKey.Backspace)
                {
                    if (curIndex > 0)
                    {
                        retString = retString.Remove(retString.Length - 1);
                        Console.Write(readKeyResult.KeyChar);
                        Console.Write(' ');
                        Console.Write(readKeyResult.KeyChar);
                        curIndex--;
                    }
                }
                else
                // handle all other keypresses
                {
                    retString += readKeyResult.KeyChar;
                    Console.Write(readKeyResult.KeyChar);
                    curIndex++;
                }
            }
            while (true);
        }

        public ScreenDrawer(IAnimalRepository animals)
        {
            _animals = animals;
            _showHelp = false;
            _table = new FormatedTable("Name", "State", "Health");
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

            _table.ClearEntries();

            foreach (var animal in _animals.GetAll())
            {
                _table.AddEntry(animal.ToString(), animal.State, $"{animal.Health}/{animal.MaxHealth}");
            }

            Console.WriteLine("Welcome  to the Zoo!!!");
            Console.WriteLine("To exit type quit");
            Console.WriteLine("To look available commands please type help");

            Console.WriteLine("=====================================================================");
            if (_showHelp) Console.WriteLine(_help);
            Console.WriteLine("=====================================================================");

            Console.WriteLine("Animals in Zoo");
            Console.WriteLine("=====================================================================");

            _table.WriteHead();
            _table.WriteEntries();

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
           + "add <new animal name, must be unique> <new animal species>\n"
           + "remove <animal name to remove, possible to remove only dead animals>\n"
           + "heal <animal name>\n"
           + "feed <animal name>\n"
           + "Specieses:\n"
           + "-Wolf\n"
           + "-Bear\n"
           + "-Tiger\n"
           + "-Lion\n"
           + "-Fox\n"
           + "-Elephant\n"
           + "For hide help type help again";

        private IAnimalRepository _animals;
        private bool _showHelp;
        private FormatedTable _table;
    }
}
