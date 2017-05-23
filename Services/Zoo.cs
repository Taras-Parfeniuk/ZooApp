using System.Timers;

using Domain.Abstract;
using Repository;
using Services.Infrastructure;
using Domain.Exceptions;

namespace Services
{
    public class Zoo
    {
        public IAnimalRepository Animals { get; private set; }
        private CycleManager _cycle;

        public Zoo()
        {
            Animals = new AnimalRepository();
            _cycle = new CycleManager(Animals);
        }

        public void AddAnimal(string name, string species)
        {
            if(Animals.Get(name) != null)
            {
                throw new NameAlreadyUsedException(name);
            }
            Animals.Add(AnimalFactory.GetAnimal(name, species));
        }

        public void FeedAnimal(string name)
        {
            Animals.Get(name).Feed();
        }

        public void HealAnimal(string name)
        {
            Animals.Get(name).Heal();
        }

        public bool RemoveAnimal(string name)
        {
            var animal = Animals.Get(name);
            if (!(animal?.IsAlive != false && animal?.IsAlive != null))
            {
                Animals.Remove(animal);
                return true;
            }
            return false;
        }
    }
}
