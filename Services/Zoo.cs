using Repository;
using Services.Infrastructure;
using Domain.Exceptions;

namespace Services
{
    public class Zoo
    {
        private CycleManager _cycle;

        public IAnimalRepository Animals { get; private set; }

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
            FeedAnimalCommand command = new FeedAnimalCommand(Animals.Get(name));
            CommandInvoker invoker = new CommandInvoker();

            invoker.SetCommand(command);
            invoker.Run();
        }

        public void HealAnimal(string name)
        {
            HealAnimalCommand command = new HealAnimalCommand(Animals.Get(name));
            CommandInvoker invoker = new CommandInvoker();

            invoker.SetCommand(command);
            invoker.Run();
        }

        public bool RemoveAnimal(string name)
        {
            var animal = Animals.Get(name);

            if (animal == null)
                throw new AnimalNotFoundException(name);

            if (!animal.IsAlive)
            {
                Animals.Remove(animal);
                return true;
            }

            return false;
        }
    }
}
