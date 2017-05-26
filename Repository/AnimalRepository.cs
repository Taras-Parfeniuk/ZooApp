using System;
using System.Collections.Generic;
using System.Linq;

using Domain.Abstract;

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
            return _animals
                .Where(a => a.GetType().Name == "Tiger")
                .Where(t => t.State == AnimalState.Sick);
        }

        public Animal GetElephantByName(string name)
        {
            return _animals
                .Where(a => a.GetType().Name == "Elephant")
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
                    Tuple.Create<string, int>(g.Key.Name, g.Where(a => a.State == AnimalState.Dead).Count()));
        }

        public IEnumerable<Animal> GetWolfsAndBearsByHealth(int health)
        {
            return _animals
                .Where(a => (a.GetType().Name == "Wolf" || a.GetType().Name == "Bear") && a.Health > health);
        }

        public IEnumerable<Animal> GetMinAndMaxHealthy()
        {
            return _animals.Where(a => a.Health == _animals.Max(x => x.Health) || a.Health == _animals.Min(x => x.Health));
        }

        public double GetHealthAverage()
        {
            return _animals.Average(a => a.Health);
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
