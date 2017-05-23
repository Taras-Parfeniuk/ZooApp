using Domain.Abstract;

namespace Domain.Entities
{
    public class Lion : Animal
    {
        public Lion(string name) : base(name)
        {
        }

        public override int MaxHealth => 5;

        public override string ToString()
        {
            return $"Lion {Name}";
        }
    }
}
