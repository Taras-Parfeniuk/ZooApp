using Domain.Abstract;

namespace Services.Infrastructure
{
    public class HealAnimalCommand : ICommand
    {
        Animal reciver;

        public HealAnimalCommand(Animal animal)
        {
            reciver = animal;
        }

        public void Execute()
        {
            reciver.Heal();
        }
    }
}
