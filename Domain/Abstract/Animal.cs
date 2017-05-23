namespace Domain.Abstract
{
    public abstract class Animal
    {
        public delegate void AnimalChangedHandler(string messsage);

        public event AnimalChangedHandler AnimalChanged;

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
                AnimalChanged($"{Name} is Satisfied");
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
            AnimalChanged($"{Name} feeling beter, but hungry");
        }

        public virtual void NextState()
        {
            switch (State)
            {
                case AnimalState.Satisfied:
                case AnimalState.Hungry:
                    State++;
                    AnimalChanged($"{Name} feeling worse");
                    break;
                case AnimalState.Sick:
                    Health--;
                    if (Health <= 0)
                    {
                        State = AnimalState.Dead;
                        AnimalChanged($"{Name} is dead(");
                    }
                    AnimalChanged($"{Name} feeling worse");
                    break;
                default:
                    break;
            }
        }

        public string Name { get; private set; }
        public int Health { get; set; }
        public AnimalState State { get; set; }
        public bool IsAlive => State != AnimalState.Dead;

        public abstract int MaxHealth { get; }
    }

    public enum AnimalState
    {
        Satisfied,
        Hungry,
        Sick,
        Dead
    }
}
