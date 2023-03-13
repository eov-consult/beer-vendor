using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVendorBeerManager
    {
        VendorBeer AddOrUpdateBeerVendor(int beerId, int vendorId, int quantity);
        ReturnedEstimateModel Estimate(EstimateDataModel data);
        IEnumerable<VendorBeer> Get();
        VendorBeer? Get(int id);
    }
}
