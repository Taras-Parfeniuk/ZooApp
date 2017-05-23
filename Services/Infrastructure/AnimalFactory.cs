using Domain.Abstract;
using Domain.Entities;
using Domain.Exceptions;

namespace Services.Infrastructure
{
    public static class AnimalFactory
    {
        public static Animal GetAnimal(string name, string species)
        {
            switch (species.ToLower())
            {
                case "wolf":
                    return new Wolf(name);
                case "lion":
                    return new Lion(name);
                case "elephant":
                    return new Elephant(name);
                case "fox":
                    return new Fox(name);
                case "tiger":
                    return new Tiger(name);
                case "bear":
                    return new Bear(name);
                default :
                    throw new SpeciesNotFoundException(species);
            }
        }
    }
}
