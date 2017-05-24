using System;

using Repository;

namespace UI
{
    public class ScreenDrawer
    {
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

        public string CommandMessage { get; set; }
        public string EventMessage { get; set; }

        public ScreenDrawer(IAnimalRepository animals)
        {
            _animals = animals;
            _showHelp = false;
            CommandMessage = String.Empty;
            EventMessage = String.Empty;
        }

        public void Draw()
        {
            Console.CursorVisible = false;
            for (var i = 0; i < Console.WindowHeight - 1; i++)
            {
                CleanLine(i);
            }

            Console.SetCursorPosition(0, 0);

            Table table = new Table("Name", "State", "Health");

            foreach (var animal in _animals.GetAll())
            {
                table.AddEntry(animal.ToString(), animal.State, $"{animal.Health}/{animal.MaxHealth}");
            }

            Console.WriteLine("Welcome  to the Zoo!!!");
            Console.WriteLine("To exit type quit");
            Console.WriteLine("To look available commands please type help");

            Console.WriteLine("=====================================================================");
            if (_showHelp) Console.WriteLine(_help);
            Console.WriteLine("=====================================================================");

            Console.WriteLine("Animals in Zoo");
            Console.WriteLine("=====================================================================");

            table.WriteHead();
            table.WriteEntries();

            Console.WriteLine("=====================================================================");
            Console.WriteLine(EventMessage);
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(CommandMessage);
            Console.WriteLine("=====================================================================");

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
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

        private void CleanLine(int line)
        {
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}
