using System.Collections.Generic;

using Domain.Abstract;
using System;

namespace Repository
{
    public delegate void RepositoryChangedHandler(string message);

    public interface IAnimalRepository
    {
        event RepositoryChangedHandler OnRepositoryChanged;

        void Add(Animal animal);
        void Remove(Animal animal);
        Animal Get(string name);
        IEnumerable<Animal> GetAll();
        void OnItemChanged(string message);
    }
}
