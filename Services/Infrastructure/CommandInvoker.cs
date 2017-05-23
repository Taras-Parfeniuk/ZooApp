using Domain.Abstract;

namespace Services.Infrastructure
{
    public class CommandInvoker
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void Run()
        {
            _command?.Execute();
        }
    }
}
