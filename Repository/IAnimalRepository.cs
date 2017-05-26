using System.Collections.Generic;
using System;

using Domain.Abstract;

namespace Repository
{
    public interface IAnimalRepository
    {
        event RepositoryChangedHandler RepositoryChanged;

        void Add(Animal animal);
        void Remove(Animal animal);
        Animal Get(string name);
        IEnumerable<Animal> GetAll();

        IEnumerable<Animal> GetByType(string typeName);
        IEnumerable<Animal> GetByState(string stateName);
        IEnumerable<Animal> GetSickTigers();
        Animal GetElephantByName(string name);
        IEnumerable<string> GetHungryNames();
        IEnumerable<Animal> GetMostHelthy();
        IEnumerable<Tuple<string, int>> GetDeadCountPerType();
        IEnumerable<Animal> GetWolfsAndBearsByHealth(int health);
        Tuple<Animal, Animal> GetMinAndMaxHealthy();
        double GetHealthAverage();
    }

    public delegate void RepositoryChangedHandler(IAnimalRepository sender, RepositoryChangedEventArgs e);

    public class RepositoryChangedEventArgs
    {
        public string Message { get; set; }

        public RepositoryChangedEventArgs(string message)
        {
            Message = message;
        }
    }
}
