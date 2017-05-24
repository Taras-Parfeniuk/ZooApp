using System.Collections.Generic;

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
