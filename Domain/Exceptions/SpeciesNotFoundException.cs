using System;

namespace Domain.Exceptions
{
    public class SpeciesNotFoundException : Exception
    {
        public SpeciesNotFoundException(string message) : base(message)
        {
        }
    }
}
