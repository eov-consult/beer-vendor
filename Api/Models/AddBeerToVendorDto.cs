namespace Api.Models
{
    public class AddBeerToVendorDto
    {
        public int BeerId { get; set; }
        public int VendorId { get; set; }
        public int Quantity { get; set; }
    }
}
