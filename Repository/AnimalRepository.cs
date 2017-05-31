using System;
using System.Collections.Generic;
using System.Linq;

using Domain.Abstract;
using Domain.Entities;

namespace Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private List<Animal> _animals = new List<Animal>();

        #region Lection 3

        public IEnumerable<Animal> GetByType(string typeName)
        {
            return _animals.Where(a => a.GetType().Name.ToLower() == typeName.ToLower());
        }

        public IEnumerable<Animal> GetByState(string stateName)
        {
            return _animals.Where(a => a.State.ToString() == stateName);
        }

        public IEnumerable<Animal> GetSickTigers()
        {
            return _animals.OfType<Tiger>()
                .Where(t => t.State == AnimalState.Sick);
        }

        public Animal GetElephantByName(string name)
        {
            return _animals.OfType<Elephant>()
                .FirstOrDefault(e => e.Name == name);
        }

        public IEnumerable<string> GetHungryNames()
        {
            return _animals
                .Where(a => a.State == AnimalState.Hungry)
                .Select(h => h.Name);
        }

        public IEnumerable<Animal> GetMostHelthy()
        {
            return _animals
                .GroupBy(o => o.GetType())
                .Select(g => 
                    g.First(a =>
                        a.Health == g.Max(i => i.Health)));
        }

        public IEnumerable<Tuple<string, int>> GetDeadCountPerType()
        {
            return _animals
                .GroupBy(o => o.GetType())
                .Select(g => 
                    Tuple.Create<string, int>(g.Key.Name, g.Count(a => a.State == AnimalState.Dead)));
        }

        public IEnumerable<Animal> GetWolfsAndBearsByHealth(int health)
        {
            return _animals
                .Where(a => (a.GetType().Name == "Wolf" || a.GetType().Name == "Bear") && a.Health > health);
        }

        public IEnumerable<Animal> GetMinAndMaxHealthy()
        {
            return from animal in _animals
                   let min = _animals.Min(a => a.Health)
                   let max = _animals.Max(a => a.Health)
                   where animal.IsAlive && (animal.Health == min || animal.Health == max)
                   select animal;
        }

        public double GetHealthAverage()
        {
            return _animals
                .Where(a => a.IsAlive)
                .Average(a => a.Health);
        }
        #endregion

        public event RepositoryChangedHandler RepositoryChanged;

        public void Add(Animal animal)
        {
            animal.AnimalChanged += OnItemChanged;
            _animals.Add(animal);
        }

        public Animal Get(string name)
        {
            return _animals.FirstOrDefault(a => a.Name == name);
        }

        public IEnumerable<Animal> GetAll()
        {
            return _animals.AsReadOnly();
        }

        public void Remove(Animal animal)
        {
            _animals.Remove(animal);
        }

        protected virtual void OnItemChanged(Animal sender, AnimalChangedEventArgs e)
        {
            RepositoryChanged(this, new RepositoryChangedEventArgs(e.Message));
        }
    }
}
