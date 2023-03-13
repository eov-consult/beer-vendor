namespace Api.Models
{
    public class VendorBeerDto
    {
        public VendorDto Vendor { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
