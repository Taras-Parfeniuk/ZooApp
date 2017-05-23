using Domain.Abstract;

namespace Domain.Entities
{
    public class Fox : Animal
    {
        public Fox(string name) : base(name)
        {
        }

        public override int MaxHealth => 3;

        public override string ToString()
        {
            return $"Fox {Name}";
        }
    }
}
