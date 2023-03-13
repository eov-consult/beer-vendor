namespace Api.Models
{
    public class EstimateDataModelDto
    {
        public int VendorId { get; set; }
        public IEnumerable<EstimateDataBeerModelDto> Beers { get; set; } = null!;
    }
    public class EstimateDataBeerModelDto
    {
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
    public class ReturnedEstimateModelDto
    {
        public EstimateDataModelDto Summary { get; set; } = null!;
        public float Amount { get; set; }
    }
}


