using Domain.Entities;

namespace Api.Models
{
    public class BeerSoldDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float AlcoholDegree { get; set; }
        public float Amount { get; set; }
        public IEnumerable<VendorBeerDto>? VendorBeers { get; set; }
        public BrewerDto Brewer { get; set; } = null!;
    }
}
