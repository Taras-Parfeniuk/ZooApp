using Domain.Abstract;

namespace Domain.Entities
{
    public class Elephant : Animal
    {
        public Elephant(string name) : base(name)
        {
        }

        public override int MaxHealth => 7;

        public override string ToString()
        {
            return $"Elephant {Name}";
        }
    }
}
