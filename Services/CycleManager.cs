using System.Threading;
using System.Linq;
using System;

using Repository;
using Domain.Abstract;
using Services.Infrastructure;

namespace Services
{
    public class CycleManager
    {
        private const int _interval = 5000;

        private Timer _timer;
        private IAnimalRepository _animals;

        public event AllAnimalsDeadEventHandler AllAnimalsDead;

        public CycleManager(IAnimalRepository animals)
        {
            _animals = animals;
            _timer = new Timer(CycleAction, null, 0, _interval);
        }

        private Animal RandomAnimal()
        {
            lock (_animals)
            {
                if (_animals.GetAll().Count() == 0)
                {
                    return null;
                }
                int toSkip = new Random().Next() % _animals.GetAll().Count();
                return _animals.GetAll()
                    .Skip(toSkip)
                    .First();
            }
        }

        private void CycleAction(object state)
        {
            CommandInvoker invoker = new CommandInvoker();
            ICommand command = new ChangeStateCommand(RandomAnimal());

            invoker.SetCommand(command);
            invoker.Run();

            if (_animals.GetAll().Where(a => !a.IsAlive).Count() == _animals.GetAll().Count() && _animals.GetAll().Count() > 0)
            {
                AllAnimalsDead(this, new AllAnimalsDeadEventArgs());
            }
        }
    }

    public delegate void AllAnimalsDeadEventHandler(object sender, AllAnimalsDeadEventArgs e);

    public class AllAnimalsDeadEventArgs : EventArgs { }
}
