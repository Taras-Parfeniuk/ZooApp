using Domain.Abstract;

namespace Services.Infrastructure
{
    public class FeedAnimalCommand : ICommand
    {
        Animal reciver;

        public FeedAnimalCommand(Animal animal)
        {
            reciver = animal;
        }

        public void Execute()
        {
            reciver.Feed();
        }
    }
}
