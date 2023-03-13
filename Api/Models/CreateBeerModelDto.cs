using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CreateBeerModelDto
    {
        public string Name { get; set; } = null!;
        public float AlcoholDegree { get; set; }
        public float Amount { get; set; }
        public int BrewerId { get; set; }
    }
}
