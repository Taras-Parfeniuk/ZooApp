using Domain.Abstract;

namespace Domain.Entities
{
    public class Tiger : Animal
    {
        public Tiger(string name) : base(name)
        {
        }

        public override int MaxHealth => 4;

        public override string ToString()
        {
            return $"Tiger {Name}";
        }
    }
}
