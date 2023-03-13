using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EstimateDataModel
    {
        public int VendorId { get; set; }
        public IEnumerable<EstimateDataBeerModel> Beers { get; set; } = null!;
    }
    public class EstimateDataBeerModel
    {
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
    public class ReturnedEstimateModel
    {
        public EstimateDataModel Summary { get; set; } = null!;
        public float Amount { get; set; }
    }
}