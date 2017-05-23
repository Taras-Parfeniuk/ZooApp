using System;
using Domain.Abstract;

namespace Services.Infrastructure
{
    public class ChangeStateCommand : ICommand
    {
        Animal reciver;
        
        public ChangeStateCommand(Animal animal)
        {
            reciver = animal;
        }

        public void Execute()
        {
            reciver?.NextState();
        }
    }
}
