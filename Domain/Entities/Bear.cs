using Domain.Abstract;

namespace Domain.Entities
{
    public class Bear : Animal
    {
        public Bear(string name) : base(name)
        {
        }

        public override int MaxHealth => 6;

        public override string ToString()
        {
            return $"Bear {Name}";
        }
    }
}
