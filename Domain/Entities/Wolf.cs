using Domain.Abstract;

namespace Domain.Entities
{
    public class Wolf : Animal
    {
        public Wolf(string name) : base(name)
        {
        }

        public override int MaxHealth => 4;

        public override string ToString()
        {
            return $"Wolf {Name}";
        }
    }
}
