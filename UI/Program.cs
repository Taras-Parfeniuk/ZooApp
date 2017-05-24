using System;

using Repository;
using Services;
using Domain.Exceptions;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            ScreenDrawer drawer = new ScreenDrawer(zoo.Animals);
            string lastCommandResult = String.Empty;

            zoo.Animals.RepositoryChanged += drawer.DrawEvent;

            while (true)
            {
                drawer.Draw();

                Console.WriteLine(lastCommandResult);
                Console.WriteLine("---------------------------------------------------------------------");

                string commandString = Console.ReadLine();
                string[] command = commandString.Split(' ');

                try
                {
                    switch (command[0])
                    {
                        case "help":
                            drawer.ShowHelp();
                            break;
                        case "add":
                            zoo.AddAnimal(command[1], command[2]);
                            lastCommandResult = $"New {command[2]} {command[1]} in zoo!";
                            break;
                        case "remove":
                            lastCommandResult = zoo.RemoveAnimal(command[1]) ? $"Bye {command[1]}(" : "Hey, it\'s alive, actually!" ;
                            break;
                        case "feed":
                            zoo.FeedAnimal(command[1]);
                            break;
                        case "heal":
                            zoo.HealAnimal(command[1]);
                            break;
                        case "quit":
                            Environment.Exit(0);
                            break;
                        default:
                            lastCommandResult = "Unknown command";
                            break;
                    }
                }
                catch (AnimalNotFoundException ex)
                {
                    lastCommandResult = $"Animal named {ex.Message} is not exist";
                }
                catch (NameAlreadyUsedException ex)
                {
                    lastCommandResult = $"Name {ex.Message} already in use";
                }
                catch (SpeciesNotFoundException ex)
                {
                    lastCommandResult = $"Unknown species named {ex.Message}";
                }
                catch (Exception ex)
                {
                    lastCommandResult = $"Error: {ex.Message}";
                }
            }
        }
    }
}
