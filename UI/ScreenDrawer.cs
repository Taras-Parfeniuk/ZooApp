using System;

using Repository;

namespace UI
{
    public class ScreenDrawer
    {
        private IAnimalRepository _animals;
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
        private bool _showHelp;

        public ScreenDrawer(IAnimalRepository animals)
        {
            _animals = animals;
            _showHelp = false;
        }

        public void Draw()
        {
            Table table = new Table("Name", "State", "Health");

            foreach (var animal in _animals.GetAll())
            {
                table.AddEntry(animal.ToString(), animal.State, $"{animal.Health}/{animal.MaxHealth}");
            }

            Console.Clear();
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
        }

        public void Draw(string eventMessage)
        {
            Draw();
            Console.WriteLine($"*{eventMessage}*");
        }

        public void ShowHelp()
        {
            _showHelp = !_showHelp;
        }
    }
}
