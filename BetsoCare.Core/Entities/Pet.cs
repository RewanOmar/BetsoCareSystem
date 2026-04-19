using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class Pet
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public string Name { get; set; } = null!;

        public PetType Type { get; set; }

        public string Breed { get; set; } = null!;

        public int Age { get; set; }

        public double Weight { get; set; }
        public string? ImageUrl { get; set; }

       
    }
}
