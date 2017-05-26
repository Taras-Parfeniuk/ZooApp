using System;
using System.Linq;

using Services;
using Domain.Exceptions;
using Domain.Abstract;
using System.Collections.Generic;

namespace UI
{
    class Program
    {
        static void OnZooClosing(object sender, ZooClosingEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("It\'s all, we are closing. Bye...");
            System.Threading.Thread.Sleep(5000);
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            ScreenDrawer drawer = new ScreenDrawer(zoo.Animals);
            string lastCommandResult = String.Empty;

            zoo.Animals.RepositoryChanged += drawer.DrawEventMessage;
            zoo.ZooClosing += OnZooClosing; 

            while (true)
            {
                drawer.CleanLine(Console.WindowHeight - 2);
                Console.SetCursorPosition(0, Console.CursorTop - 1);

                drawer.CommandMessage = lastCommandResult;
                drawer.Draw();

                string commandString = drawer.ReadLineWithControl();
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
                        case "all":
                            drawer.SetDataSource(zoo.Animals.GetAll());
                            break;
                        case "GetByType":
                            drawer.SetDataSource(zoo.Animals.GetByType(command[1]));
                            break;
                        case "GetByState":
                            drawer.SetDataSource(zoo.Animals.GetByState(command[1]));
                            break;
                        case "GetSickTigers":
                            drawer.SetDataSource(zoo.Animals.GetSickTigers());
                            break;
                        case "GetElephantByName":
                            drawer.SetDataSource(new List<Animal>() { zoo.Animals.GetElephantByName(command[1]) });
                            break;
                        case "GetHungryNames": 
                            lastCommandResult = String.Empty;

                            foreach (var a in zoo.Animals.GetHungryNames())
                            {
                                lastCommandResult += $"{a}, ";
                            }
                            lastCommandResult.Remove(lastCommandResult.Count() - 1, 1);
                            break;
                        case "GetMostHelthy":
                            drawer.SetDataSource(zoo.Animals.GetMostHelthy());
                            break;
                        case "GetDeadCountPerType":
                            lastCommandResult = String.Empty;

                            foreach(var t in zoo.Animals.GetDeadCountPerType())
                            {
                                lastCommandResult += $"{t.Item1}: {t.Item2}\n";
                            }
                            break;
                        case "GetWolfsAndBearsByHealth":
                            drawer.SetDataSource(zoo.Animals.GetWolfsAndBearsByHealth(3));
                            break;
                        case "GetMinAndMaxHealthy":
                            drawer.SetDataSource(zoo.Animals.GetMinAndMaxHealthy());
                            break;
                        case "GetHealthAverage":
                            lastCommandResult = zoo.Animals.GetHealthAverage().ToString();
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
