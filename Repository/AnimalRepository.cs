using System;
using System.Collections.Generic;
using System.Linq;

using Domain.Abstract;
using Domain.Exceptions;

namespace Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        public event RepositoryChangedHandler OnRepositoryChanged;

        private List<Animal> _animals = new List<Animal>();

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

        public void OnItemChanged(string message)
        {
            OnRepositoryChanged(message);
        }
    }
}
