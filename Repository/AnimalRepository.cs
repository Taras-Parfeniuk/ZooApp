using System.Collections.Generic;
using System.Linq;

using Domain.Abstract;

namespace Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        public event RepositoryChangedHandler RepositoryChanged;

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

        protected virtual void OnItemChanged(Animal sender, AnimalChangedEventArgs e)
        {
            RepositoryChanged(this, new RepositoryChangedEventArgs(e.Message));
        }
    }
}
