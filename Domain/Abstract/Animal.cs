using System;

namespace Domain.Abstract
{
    public abstract class Animal
    {
        public event AnimalChangedHandler AnimalChanged;

        public string Name { get; private set; }
        public int Health { get; set; }
        public AnimalState State { get; set; }
        public abstract int MaxHealth { get; }

        public bool IsAlive => State != AnimalState.Dead;


        public Animal(string name)
        {
            Name = name;
            Health = MaxHealth;
            State = AnimalState.Satisfied;
        }

        public virtual void Feed()
        {
            if (State == AnimalState.Hungry)
            {
                State = AnimalState.Satisfied;
                AnimalChanged(this, new AnimalChangedEventArgs($"{Name} is Satisfied"));
            }
        }

        public virtual void Heal()
        {
            if (Health < MaxHealth && IsAlive)
            {
                Health++;
            }
            if (State == AnimalState.Sick)
                State = AnimalState.Hungry;
            AnimalChanged(this, new AnimalChangedEventArgs($"{Name} feeling beter, but hungry"));
        }

        public virtual void NextState()
        {
            switch (State)
            {
                case AnimalState.Satisfied:
                case AnimalState.Hungry:
                    State++;
                    AnimalChanged(this, new AnimalChangedEventArgs($"{Name} feeling worse"));
                    break;
                case AnimalState.Sick:
                    Health--;
                    if (Health <= 0)
                    {
                        State = AnimalState.Dead;
                        AnimalChanged(this, new AnimalChangedEventArgs($"{Name} is dead("));
                    }
                    AnimalChanged(this, new AnimalChangedEventArgs($"{Name} feeling worse"));
                    break;
                default:
                    break;
            }
        }
    }

    public delegate void AnimalChangedHandler(Animal sender, AnimalChangedEventArgs e);

    public class AnimalChangedEventArgs : EventArgs
    {
        public string Message { get; set; }

        public AnimalChangedEventArgs(string message)
        {
            Message = message;
        }
    }

    public enum AnimalState
    {
        Satisfied,
        Hungry,
        Sick,
        Dead
    }
}
